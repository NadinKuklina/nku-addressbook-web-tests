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
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        public string Firstname { get; set; }
       
        public string Lastname { get; set; }

        public string Id { get; set; }


        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname && Lastname == other.Lastname;
        }

        public override int GetHashCode()
        {
            //return 0;
            //return Firstname.GetHashCode();
            var hash = 1;
            hash = hash * (Firstname.GetHashCode() + Lastname.GetHashCode());
            return hash;
        }

        public override string ToString()
        {
            return "FirstName=" + Firstname + ", LastName=" + Lastname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            int result = this.Firstname.CompareTo(other.Firstname);

            if (result != 0)
            {
                return result;
            }
            else
            {
                return this.Lastname.CompareTo(other.Lastname);
            }
        }
    }
}
