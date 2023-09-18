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
    public class AccountData
    {
        private string username;
        private string password;

        public string Username 
        { get
            {
                return username;
            } 
            set
            {
                username = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public AccountData(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
