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
     public class GroupData : IEquatable<GroupData>
    {
        private string name;
        private string header = "";
        private string footer = "";

        public GroupData(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Header
        { get { return header; } 
          set { header = value; } 
        }
        public string Footer
        {
            get { return footer; }
            set { footer = value; }
        }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;   
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }

        public int GetHashCode()
        {
            //return 0;
            return Name.GetHashCode();
        }
    }
}
