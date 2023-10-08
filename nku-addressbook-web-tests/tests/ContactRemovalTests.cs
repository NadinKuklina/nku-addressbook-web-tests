using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void RemoveContact()
        {    
            int i = 0;

            app.Navigator.GoToHomePage();

            if (app.Contacts.iContactsCount() < i+1)
            {
                while (app.Contacts.iContactsCount() < i+1)
                {
                    ContactData contact = new ContactData("first name", "last name");

                    app.Contacts.CreateContact(contact);
                }
            }

            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.RemoveByIndex(i);
            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.RemoveAt(i);

            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
