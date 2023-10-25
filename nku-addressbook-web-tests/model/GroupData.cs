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
     public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        
        public GroupData(string name)
        {
            Name = name;
        }
        public GroupData()
        {           
        }
        public string Name { get; set; }
        
        public string Header { get; set; }
        
        public string Footer { get; set; }       
        
        public string Id { get; set; }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name); 
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
            //return Name.Equals(other.Name, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            //return 0;
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + Name + "\nheader=" + Header + "\nfooter=" + Footer;
        }
    }
}
