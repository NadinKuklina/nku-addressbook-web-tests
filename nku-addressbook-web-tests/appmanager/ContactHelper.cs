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
    public class ContactHelper : HelperBase
    {     
        public ContactHelper(ApplicationManager manager) : base(manager)
        {            
        }

        public ContactHelper RemoveByIndex(int i)
        {
            manager.Navigator.GoToHomePage();
            if(!IsElementPresent(By.XPath("//tr[@name='entry']")))
            {
                ContactData contact = new ContactData("first name", "last name");
                InitContactCreation();
                FillContactForm(contact);
                SubmitContact();
                ReturnToHomePage();
            }
            SelectContactByIndex(i);
            DeleteContact();
            ConfirmYesInAlert();
            return this;
        }

        public ContactHelper ModifyByIndex(int i, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            if (!IsElementPresent(By.XPath("//tr[@name='entry']")))
            {
                ContactData contact = new ContactData("first name", "last name");
                InitContactCreation();
                FillContactForm(contact);
                SubmitContact();
                ReturnToHomePage();
            }
            InitContactModificationByIndex(i);
            FillContactForm(newData);
            SubmitContactModification();
            
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContactHelper InitContactModificationByIndex(int i)
        {
            int newi = i + 1;
            driver.FindElement(By.XPath("//tr["+newi+"]/td/a[contains(@href,'edit.php?')]")).Click();
            return this;
        }

        public ContactHelper ConfirmYesInAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.CssSelector("input[value='Delete']")).Click();
            return this;
        }

        public ContactHelper SelectContactByIndex(int i)
        {
            int newi = i + 1;
            driver.FindElement(By.XPath("//tr["+newi+"]/td/input[@name='selected[]']")).Click();
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);            
            
            return this;
        }
        public ContactHelper SubmitContact()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
    }
}
