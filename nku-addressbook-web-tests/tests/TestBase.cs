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
    public class TestBase
    {
        protected ApplicationManager app;

        [SetUp]
        public void SetupTest()
        {
            app = ApplicationManager.GetIntance();
            System.Console.Out.Write("Сработал не глобальный SetupTest ");
        }     

        /*
        [TearDown]
        public void TearDownTest()
        {
            app.Driver.Quit();
            System.Console.Out.Write("Cработал driver.Quit ");
        }
        */
    }
}
