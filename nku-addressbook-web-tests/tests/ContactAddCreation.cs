using System;
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
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.InitContactCreation();
            ContactData contact = new ContactData("nku1", "lastname2");
            app.Contacts.FillContactForm(contact);
            app.Contacts.SubmitContact();
            //ReturnToHomePage();
            app.Auth.Logout();           
        } 
    }
}
