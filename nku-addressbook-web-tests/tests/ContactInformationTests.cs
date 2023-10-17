using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            int index = 0;
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(index);
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(index);

            //varification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestContactDetailPage()
        {
            int index = 1;
            ContactData fromForm = app.Contacts.GetContactInformationFromEditForm(index);
            ContactData fromDetailForm = app.Contacts.GetContactInformationFromDetailPage(index);

            //varification
            Assert.AreEqual(fromForm.FI, fromDetailForm.FI);
        }
    }
}
