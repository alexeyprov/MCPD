using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;

using DataAccess;
using NUnit.Framework;

namespace DataAccessTest
{
	[TestFixture]
	public class TestHarness
	{
		IList<IContact> _contacts;

		[SetUp]
		public void PrepareContacts()
		{
			if (null == _contacts)
			{
				using (Stream stm = Assembly.GetExecutingAssembly().GetManifestResourceStream("DataAccessTest.ContactList.xml"))
				{
					DataSet ds = new DataSet();
					ds.ReadXml(stm);
					_contacts = Contact.Load(ds);
				}
			}
		}

		[Test]
		public void TestFirstName()
		{
			string[] expectedNames = new string[] { "Sayed", "Sayed", "Mike" };
			Assert.AreEqual(expectedNames.Length, _contacts.Count);

			for (int i = 0; i < expectedNames.Length; ++i)
			{
				Assert.AreEqual(expectedNames[i], _contacts[i].FirstName);
			}
		}

		[Test]
		public void TestMiddleName()
		{
			string[] expectedNames = new string[] { "Ibrahim", "Yahya", "Ray" };
			Assert.AreEqual(expectedNames.Length, _contacts.Count);

			for (int i = 0; i < expectedNames.Length; ++i)
			{
				Assert.AreEqual(expectedNames[i], _contacts[i].MiddleName);
			}
		}

		[Test]
		public void TestLastName()
		{
			string[] expectedNames = new string[] { "Hashimi", "Hashimi", "Murphy" };
			Assert.AreEqual(expectedNames.Length, _contacts.Count);

			for (int i = 0; i < expectedNames.Length; ++i)
			{
				Assert.AreEqual(expectedNames[i], _contacts[i].LastName);
			}
		}

		[Test]
		public void TestSsn()
		{
			string[] expectedSsns = new string[] { "111-11-1111", "222-22-2222", "333-33-3333" };
			Assert.AreEqual(expectedSsns.Length, _contacts.Count);

			for (int i = 0; i < expectedSsns.Length; ++i)
			{
				Assert.AreEqual(expectedSsns[i], _contacts[i].Ssn);
			}
		}

		[Test]
		public void TestEmail()
		{
			string[] expectedEmails = new string[] { "sayed.hashimi@gmail.com", "sayed@sayedhashimi.com", "magickmike@gmail.com" };
			Assert.AreEqual(expectedEmails.Length, _contacts.Count);

			for (int i = 0; i < expectedEmails.Length; ++i)
			{
				Assert.AreEqual(expectedEmails[i], _contacts[i].Email);
			}
		}
	}
}
