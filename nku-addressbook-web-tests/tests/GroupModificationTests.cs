using System;
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
    public class GroupModificationTests : AuthTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            int i = 0;

            app.Navigator.GoToGroupPage();

            if (app.Groups.iGroupsCount() < i+1)
            {
                while (app.Groups.iGroupsCount() < i+1)
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

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.ModifyByIndex(i, newData);

            Assert.AreEqual(oldGroups.Count, app.Groups.iGroupsCount());

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[i].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
