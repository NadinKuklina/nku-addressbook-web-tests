﻿using System;
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
    public class ContactModificationTetsts : AuthTestBase 
    {
        [Test]
        public void ContactModificationTest()
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

            ContactData newData = new ContactData("newFirstName", "newLastName");

            List<ContactData> oldContacts = app.Contacts.GetContactsList();
            ContactData oldContact = oldContacts[i];

            app.Contacts.ModifyByIndex(i, newData);

            Assert.AreEqual(oldContacts.Count, app.Contacts.iContactsCount());

            oldContacts[i].Firstname = newData.Firstname;
            oldContacts[i].Lastname = newData.Lastname;
            oldContacts.Sort();
            List<ContactData> newContacts = app.Contacts.GetContactsList();            
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == oldContact.Id)
                {
                    Assert.AreEqual(newData.Firstname, contact.Firstname);
                }
            }
        }
    }
}
