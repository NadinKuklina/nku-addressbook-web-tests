﻿using System;
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
    public class ContactHelper : HelperBase
    {
        private List<ContactData> contactCache = null;

        public ContactHelper(ApplicationManager manager) : base(manager)
        {            
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));

            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhone = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhone
            };
        }

        public string GetContactInformationFromEditFormForDetail(int index)
        {
            string result = string.Empty;

            ContactData contactData = GetContactInformationFromEditForm(index);

            result = result + contactData.FI;

            if (!string.IsNullOrEmpty(contactData.Address))
            {
                result = result + "\r\n"+contactData.Address;
            }

            if (!string.IsNullOrEmpty(contactData.HomePhone) 
                || !string.IsNullOrEmpty(contactData.MobilePhone) 
                || !string.IsNullOrEmpty(contactData.WorkPhone))
            {
                result = result + "\r\n";
                if (!string.IsNullOrEmpty(contactData.HomePhone))
                {
                    result = result + "\r\nH: " + contactData.HomePhone;
                }
                if (!string.IsNullOrEmpty(contactData.MobilePhone))
                {
                    result = result + "\r\nM: " + contactData.MobilePhone;
                }
                if (!string.IsNullOrEmpty(contactData.WorkPhone))
                {
                    result = result + "\r\nW: " + contactData.WorkPhone;
                }
            }                 
                       
             if (!string.IsNullOrEmpty(contactData.Email1) 
                || !string.IsNullOrEmpty(contactData.Email2) 
                || !string.IsNullOrEmpty(contactData.Email3))
            {
                result = result + "\r\n";
                if (!string.IsNullOrEmpty(contactData.Email1))
                {
                    result = result + "\r\n" + contactData.Email1;
                }
                if (!string.IsNullOrEmpty(contactData.Email2))
                {
                    result = result + "\r\n" + contactData.Email2;
                }
                if (!string.IsNullOrEmpty(contactData.Email3))
                {
                    result = result + "\r\n" + contactData.Email3;
                }
            }                  

            result = result + "\r\n\r\n\r\n\r\n\r\n\r\n\r\n";

            return result;
        }

        public string GetContactInformationFromDetailPage(int index)
        {
            manager.Navigator.GoToHomePage();
            OpenDetailInformationByIndex(index);
            string infoFromDetalPage = driver.FindElement(By.XPath("//div[@id='content']")).GetAttribute("outerText");
            return infoFromDetalPage;
        }

        public ContactHelper OpenDetailInformationByIndex(int index)
        {
            driver.FindElement(By.XPath("//tr[" + (index + 2) + "][@name='entry']/td[@class='center']/a[contains(@href,'view.php?')]")).Click();            
            return this;
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModificationByIndex(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");            
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            string fiFromEditForm = string.Empty;

            if (!string.IsNullOrEmpty(firstName))
            {
                fiFromEditForm = firstName;
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                if (string.IsNullOrEmpty(fiFromEditForm))
                {
                    fiFromEditForm = lastName;
                }
                else
                {
                    fiFromEditForm = fiFromEditForm + " " + lastName;
                }
            }

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email1 = email1,
                Email2 = email2,
                Email3 = email3,
                FI = fiFromEditForm
            };
        }

        public ContactHelper CreateContact(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            InitContactCreation();
            FillContactForm(contact);
            SubmitContact();
            ReturnToHomePage();
            return this;
        }

        public List<ContactData> GetContactsList()
        {
            if(contactCache == null)
            {
                contactCache = new List<ContactData>();

                manager.Navigator.GoToHomePage();

                if (IsElementPresent(By.XPath("//tr[@name='entry']")))
                {
                    string fname;
                    string lname;
                    string lid;

                    ICollection<IWebElement> elements = driver.FindElements(By.XPath("//tr[@name='entry']"));

                    for (int i = 0; i < elements.Count; i++)
                    {
                        fname = driver.FindElement(By.XPath("//tr[" + (i + 2) + "][@name='entry']/td[3]")).Text;
                        lname = driver.FindElement(By.XPath("//tr[" + (i + 2) + "][@name='entry']/td[2]")).Text;
                        lid = driver.FindElement(By.XPath("//tr[" + (i + 2) + "]/td[@class='center']/input")).GetAttribute("value");

                        contactCache.Add(new ContactData(fname, lname)
                        {
                            Id = lid
                        });
                    }
                }
            }           
           
            return new List<ContactData>(contactCache);
        }

        public ContactHelper RemoveByIndex(int i)
        {
            manager.Navigator.GoToHomePage();           
            SelectContactByIndex(i);
            DeleteContact();
            ConfirmYesInAlert();
            return this;
        }

        public ContactHelper ModifyByIndex(int i, ContactData newData)
        {
            manager.Navigator.GoToHomePage();           
            InitContactModificationByIndex(i);
            FillContactForm(newData);
            SubmitContactModification();
            
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModificationByIndex(int i)
        {            
            driver.FindElement(By.XPath("//tr["+(i+2)+"]/td/a[contains(@href,'edit.php?')]")).Click();
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
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContactByIndex(int i)
        {            
            driver.FindElement(By.XPath("//tr["+(i+2)+"]/td/input[@name='selected[]']")).Click();
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
            contactCache = null;
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
