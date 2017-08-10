//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Tests.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Security.Principal;
    using System.Threading;
    using System.Web.Profile;
    using System.Web.Security;
    using DataAccessLayer;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;
    using ExpensesDataContext = DataAccessLayer.ExpensesDataContext;

    [TestClass]
    public class UserRepositoryFixture
    {
        private static readonly string TestDatabaseConnectionString = ConfigurationManager.ConnectionStrings["aExpense"].ConnectionString;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            using (var db = new ExpensesDataContext(TestDatabaseConnectionString))
            {
                if (db.DatabaseExists())
                {
                    db.DeleteDatabase();
                }
            }

            string winPath = Environment.GetEnvironmentVariable("windir");
            string aspnetDb = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}{1}{2}", winPath, Path.DirectorySeparatorChar, @"Microsoft.NET\Framework\v2.0.50727\aspnet_regsql.exe");
            string args = "-C \"" + TestDatabaseConnectionString + "\" -A all";

            Process process = Process.Start(aspnetDb, args);
            process.WaitForExit();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            using (var db = new ExpensesDataContext(TestDatabaseConnectionString))
            {
                if (db.DatabaseExists())
                {
                    db.DeleteDatabase();
                }
            }
        }

        [TestMethod]
        public void GetUser()
        {
            string username = "ADATUM\\johndoe";
            var roles = new[] { "GetUserTest_Role" };
            DatabaseHelper.CreateUserInDatabase(username, roles);
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), roles);
            
            var repository = new UserRepository();
            var user = repository.GetUser(username);

            Assert.IsNotNull(user);
            Assert.AreEqual(username, user.UserName);
            Assert.AreEqual(1, user.Roles.Count);
            Assert.AreEqual("GetUserTest_Role", user.Roles[0]);
            Assert.AreEqual("John Doe", user.FullName);
            Assert.AreEqual("ADATUM\\mary", user.Manager);
            Assert.AreEqual("31023", user.CostCenter);
            Assert.AreEqual(ReimbursementMethod.Check, user.PreferredReimbursementMethod);
        }

        [TestMethod]
        public void UpdatePreferredReimbursementMethod()
        {
            string username = "ADATUM\\mary";
            var roles = new[] { "UpdatePreferredReimbursementMethod_Role" };
            DatabaseHelper.CreateUserInDatabase(username, roles);
            Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username), roles);
            var repository = new UserRepository();

            var actual = repository.GetUser(username);
            actual.PreferredReimbursementMethod = ReimbursementMethod.Cash;

            repository.UpdateUserPreferredReimbursementMethod(actual);

            var updated = repository.GetUser(username);

            Assert.IsNotNull(updated);
            Assert.AreEqual(ReimbursementMethod.Cash, updated.PreferredReimbursementMethod);
        }

        private static class DatabaseHelper
        {
            public static void CreateUserInDatabase(string userName, IEnumerable<string> roles)
            {
                MembershipCreateStatus status;
                Membership.CreateUser(userName, "Passw0rd!", string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}@test.com", userName), "question", "answer", true, out status);
                foreach (var role in roles)
                {
                    Roles.CreateRole(role);
                    Roles.AddUserToRole(userName, role);
                }

                var profile = ProfileBase.Create(userName);
                profile.SetPropertyValue("PreferredReimbursementMethod", "Check");
                profile.Save();
            }
        }
    }
}