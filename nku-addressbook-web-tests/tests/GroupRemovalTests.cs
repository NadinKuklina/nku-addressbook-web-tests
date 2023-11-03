using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void GroupRemovalTest()
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

            List<GroupData> oldGroups = GroupData.GetAll(); //app.Groups.GetGroupList();
            GroupData toBeRemoved = oldGroups[i];

            app.Groups.Remove(toBeRemoved);

            Assert.AreEqual(oldGroups.Count - 1, app.Groups.iGroupsCount());

            List<GroupData> newGroups = GroupData.GetAll(); //app.Groups.GetGroupList()
           
            oldGroups.RemoveAt(i);
            oldGroups.Sort();
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }




        }
    }
}
