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
    public class ContactTests : AuthTestBase
    {     
        [Test]
        public void ContactCreationTest()
        {             
            ContactData contact = new ContactData("nku2", "lastname2");

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.CreateContact(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.iContactsCount());

            oldContacts.Add(contact);
            oldContacts.Sort();
            List<ContactData> newContacts = app.Contacts.GetContactsList();          
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);  
        } 
    }
}
