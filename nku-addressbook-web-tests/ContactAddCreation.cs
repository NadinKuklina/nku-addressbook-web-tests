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
            navigator.GoToHomePage();
            loginHelper.Login(new AccountData("admin", "secret"));
            contactHelper.InitContactCreation();
            ContactData contact = new ContactData("nku1", "lastname2");
            contactHelper.FillContactForm(contact);
            contactHelper.SubmitContact();
            //ReturnToHomePage();
            loginHelper.Logout();           
        } 
    }
}
