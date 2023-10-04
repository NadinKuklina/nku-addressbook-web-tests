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
    public class ContactModificationTetsts : AuthTestBase 
    {
        [Test]
        public void ContactModificationTest()
        {
            int i = 1;

            app.Navigator.GoToHomePage();

            if (app.Driver.FindElements(By.XPath("//tr[@name='entry']")).Count < i)
            {
                while (app.Driver.FindElements(By.XPath("//tr[@name='entry']")).Count < i)
                {
                    ContactData contact = new ContactData("first name", "last name");

                    app.Contacts.CreateContact(contact);
                }
            }

            ContactData newData = new ContactData("newFirstName", "newLastName");            

            app.Contacts.ModifyByIndex(i, newData);
        }
    }
}
