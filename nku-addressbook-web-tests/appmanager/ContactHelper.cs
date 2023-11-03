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

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void SelectContact(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public string GetContactInformationFromEditFormForDetail(int index)
        {
            string result = "";

            ContactData contactData = GetContactInformationFromEditForm(index);

            result = result + contactData.FI
                + contactData.Address;

            if (!string.IsNullOrEmpty(contactData.HomePhone))
            {
                result = result + "H: " + contactData.HomePhone;
            }
            if (!string.IsNullOrEmpty(contactData.MobilePhone))
            {
                result = result + "M: " + contactData.MobilePhone;
            }
            if (!string.IsNullOrEmpty(contactData.WorkPhone))
            {
                result = result + "W: " + contactData.WorkPhone;
            }

            result = result
                + contactData.Email1
                + contactData.Email2
                + contactData.Email3;

            return result;
        }

        public string GetContactInformationFromDetailPage(int index)
        {
            string fiFromDetail = string.Empty;
            string addressFromDetail = string.Empty;
            string phonesFromDetails = string.Empty;
            string emailsFromDetail = string.Empty;
            string result = "";

            manager.Navigator.GoToHomePage();
            OpenDetailInformationByIndex(index);

            string infoFromDetalPage = driver.FindElement(By.XPath("/html//div[@id='content']")).Text;
            fiFromDetail = driver.FindElement(By.XPath("/html//div[@id='content']/b")).GetAttribute("textContent").Trim();
            
            string pattern = @"\r\n";
            string[] arrayOfStrings = Regex.Split(infoFromDetalPage, pattern);
                                    
            Regex rPatternHomePhone = new Regex("H: ");
            Regex rPatternMobilePhone = new Regex("M: ");
            Regex rPatternWorkPhone = new Regex("W: ");
            //Regex rEmails = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");

            for (int i = 0; i < arrayOfStrings.Length; i++)
            {
                if (!string.IsNullOrEmpty(arrayOfStrings[i]) 
                    && arrayOfStrings[i] != " "                     
                    && !rPatternHomePhone.IsMatch(arrayOfStrings[i])
                    && !rPatternMobilePhone.IsMatch(arrayOfStrings[i])
                    && !rPatternWorkPhone.IsMatch(arrayOfStrings[i])
                    && !isThisLink(arrayOfStrings[i]))
                {
                    if (!string.IsNullOrEmpty(fiFromDetail))
                    {
                        Regex rPatternFI = new Regex(fiFromDetail);

                        if (!rPatternFI.IsMatch(arrayOfStrings[i]))
                        {
                            addressFromDetail = arrayOfStrings[i];
                        }                        
                    }    
                    else
                    {
                        addressFromDetail = arrayOfStrings[i];
                    }
                }

                if (rPatternHomePhone.IsMatch(arrayOfStrings[i]))
                {
                    phonesFromDetails = phonesFromDetails + arrayOfStrings[i];
                }

                if (rPatternMobilePhone.IsMatch(arrayOfStrings[i]))
                {
                    phonesFromDetails = phonesFromDetails + arrayOfStrings[i];
                }

                if (rPatternWorkPhone.IsMatch(arrayOfStrings[i]))
                {
                    phonesFromDetails = phonesFromDetails + arrayOfStrings[i];
                }

                if (isThisLink(arrayOfStrings[i]))
                {
                    
                    emailsFromDetail = emailsFromDetail + arrayOfStrings[i];
                }
            }

            result = fiFromDetail + addressFromDetail + phonesFromDetails + emailsFromDetail;

            return result;
        }

        public bool isThisLink(string text)
        {
            if(IsElementPresent(By.XPath("//div[@id='content']/a[contains(@href,'" + text + "')]")))
            {
                return true;
            }
            else
            {
                return false;
            }            
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
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("email"), contact.Email1);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);

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
