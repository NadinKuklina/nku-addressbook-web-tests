﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactTests : ContactTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contact = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contact.Add(new ContactData(GenerateRandomString(10), GenerateRandomString(10))
                {
                    Address = GenerateRandomString(30),
                    HomePhone = RandomDigits(9),
                    MobilePhone = RandomDigits(9),
                    WorkPhone = RandomDigits(9),
                    Email1 = GenerateRandomEmail(),
                    Email2 = GenerateRandomEmail(),
                    Email3 = GenerateRandomEmail()
                });
            }
            return contact;
        }
        public static IEnumerable<ContactData> ContactDataFromXMLFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                    .Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static string GenerateRandomEmail()
        {
            int l = Convert.ToInt32(rnd.NextDouble());
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223)));
            }
            return builder.ToString();
        }

       
        [Test, TestCaseSource("ContactDataFromXMLFile")]
        public void ContactCreationTest(ContactData contact)
        {
            List<ContactData> oldContacts = ContactData.GetAll(); //app.Contacts.GetContactsList();

            app.Contacts.CreateContact(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.iContactsCount());

            oldContacts.Add(contact);
            oldContacts.Sort();
            List<ContactData> newContacts = ContactData.GetAll(); //app.Contacts.GetContactsList();          
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);  
        } 
    }
}
