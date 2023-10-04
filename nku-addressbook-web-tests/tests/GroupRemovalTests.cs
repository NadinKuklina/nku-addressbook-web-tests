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
    public class GroupRemovalTests : AuthTestBase
    {
        [Test]
        public void GroupRemovalTest()
        {        
            int i = 1;

            app.Navigator.GoToGroupPage();                 

            if (app.Driver.FindElements(By.CssSelector("span.group")).Count < i)
            {
                while (app.Driver.FindElements(By.CssSelector("span.group")).Count < i)
                {
                    GroupData group = new GroupData("testgroupsname");
                    group.Header = "testgroupsheader";
                    group.Footer = "testgroupsfooter";

                    app.Groups.Create(group);
                }
            }
            app.Groups.RemoveByIndex(i);                           
        }
    }
}
