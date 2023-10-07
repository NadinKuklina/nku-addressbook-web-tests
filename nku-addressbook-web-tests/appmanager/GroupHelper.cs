using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {    
        public GroupHelper(ApplicationManager manager) 
            : base(manager)
        {           
        }       

        public GroupHelper Create(GroupData group)
        {
            manager.Navigator.GoToGroupPage();

            InitGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupPage();
            return this;
        }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> groups = new List<GroupData>();

            manager.Navigator.GoToGroupPage();
            ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
            foreach (IWebElement element in elements)
            {                
                groups.Add(new GroupData(element.Text));
            }
            return groups;
        }

        public GroupHelper ModifyByIndex(int i, GroupData newData)
        {
            manager.Navigator.GoToGroupPage();            
            SelectGroup(i);
            InitGroupModification();
            FillGroupForm(newData);
            SubmitGroupModification();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper SubmitGroupModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public GroupHelper InitGroupModification()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper RemoveByIndex(int i)
        {
            manager.Navigator.GoToGroupPage();            
            SelectGroup(i);                      
            RemoveGroup();
            ReturnToGroupPage();
            return this;
        }

        public GroupHelper InitGroupCreation()
        {
            manager.Navigator.GoToGroupPage();
            driver.FindElement(By.Name("new")).Click();
            return this;
        }

        public GroupHelper FillGroupForm(GroupData group)
        {    
            Type(By.Name("group_name"), group.Name);
            Type(By.Name("group_header"), group.Header);
            Type(By.Name("group_footer"), group.Footer);
            
            return this;
        }      

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }
        public GroupHelper ReturnToGroupPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
            //driver.FindElement(By.LinkText("Logout")).Click();
            return this;
        }
        public GroupHelper SelectGroup(int index)
        {
            if (IsElementPresent(By.XPath("//div[@id='content']/form/span[" + index + "]/input")))
            {
                driver.FindElement(By.XPath("//div[@id='content']/form/span[" + index + "]/input")).Click();
            }
            else
            {

                driver.FindElement(By.XPath("//div[@id='content']/form/span[1]/input")).Click();
            }           
            return this;
        }

        public GroupHelper RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }        
    }
}
