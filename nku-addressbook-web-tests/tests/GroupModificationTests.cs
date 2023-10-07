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
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            int i = 1;

            app.Navigator.GoToGroupPage();

            if (app.Groups.iGroupsCount() < i)
            {
                while (app.Groups.iGroupsCount() < i)
                {
                    GroupData group = new GroupData("testgroupsname");
                    group.Header = "testgroupsheader";
                    group.Footer = "testgroupsfooter";

                    app.Groups.Create(group);
                }
            }

            GroupData newData = new GroupData("editgroup2");
            newData.Header = null;
            newData.Footer = null;

            app.Groups.ModifyByIndex(i, newData);
        }
    }
}
