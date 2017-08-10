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
    using System.Collections.Generic;
    using System.Threading;
    using Claims;
    using DataAccessLayer;
    using Microsoft.IdentityModel.Claims;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Model;

    [TestClass]
    public class UserRepositoryFixture
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            WebRole.ProfileInitializer.Initialize();
        }

        [TestMethod]
        public void GetUser()
        {
            string username = "ADATUM\\johndoe";
            var identity = GetStubIdentity(username);
            Thread.CurrentPrincipal = new ClaimsPrincipal(identity);

            var repository = new UserRepository();
            var user = repository.GetUser(username);

            Assert.IsNotNull(user);
            Assert.AreEqual(username, user.UserName);
            Assert.AreEqual(1, user.Roles.Count);
            Assert.AreEqual("GetUserTest_Role", user.Roles[0]);
            Assert.AreEqual("John Doe", user.FullName);
            Assert.AreEqual("ADATUM\\mary", user.Manager);
            Assert.AreEqual("31023", user.CostCenter);
            Assert.AreEqual(ReimbursementMethod.Cash, user.PreferredReimbursementMethod);
        }

        [TestMethod]
        public void UpdatePreferredReimbursementMethod()
        {
            string username = "ADATUM\\mary";
            var identity = GetStubIdentity(username);
            Thread.CurrentPrincipal = new ClaimsPrincipal(identity);

            var repository = new UserRepository();
            var actual = repository.GetUser(username);
            actual.PreferredReimbursementMethod = ReimbursementMethod.Cash;
            repository.UpdateUserPreferredReimbursementMethod(actual);

            var updated = repository.GetUser(username);
            Assert.AreEqual(ReimbursementMethod.Cash, updated.PreferredReimbursementMethod);

            actual.PreferredReimbursementMethod = ReimbursementMethod.DirectDeposit;
            repository.UpdateUserPreferredReimbursementMethod(actual);

            updated = repository.GetUser(username);
            Assert.AreEqual(ReimbursementMethod.DirectDeposit, updated.PreferredReimbursementMethod);
        }

        private static ClaimsIdentity[] GetStubIdentity(string userName)
        {
            var claims = new List<Claim>
                           {
                               new Claim(System.IdentityModel.Claims.ClaimTypes.Name, userName), 
                               new Claim(System.IdentityModel.Claims.ClaimTypes.GivenName, "John"),
                               new Claim(System.IdentityModel.Claims.ClaimTypes.Surname, "Doe"),
                               new Claim(Adatum.ClaimTypes.CostCenter, "31023"),
                               new Claim(ClaimTypes.Role, "GetUserTest_Role"),
                               new Claim(Adatum.ClaimTypes.Manager, "ADATUM\\mary")
                           };

            var identity = new[] { new ClaimsIdentity(claims) };

            return identity;
        }
    }
}