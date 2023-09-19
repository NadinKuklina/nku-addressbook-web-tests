﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactTests : TestBase
    {     
        [Test]
        public void ContactCreationTest()
        {             
            ContactData contact = new ContactData("nku1", "lastname2");
            app.Contacts                
                .InitContactCreation()
                .FillContactForm(contact)
                .SubmitContact();                                    
        } 
    }
}