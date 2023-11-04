using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

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

            List<ContactData> oldContacts = ContactData.GetAll(); //app.Contacts.GetContactsList();
            ContactData toBeRemoved = oldContacts[i];

            app.Contacts.Remove(toBeRemoved); //RemoveByIndex(i);

            Assert.AreEqual(oldContacts.Count-1, app.Contacts.iContactsCount());

            oldContacts.RemoveAt(i);
            oldContacts.Sort();

            List<ContactData> newContacts = ContactData.GetAll(); //app.Contacts.GetContactsList();    
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
