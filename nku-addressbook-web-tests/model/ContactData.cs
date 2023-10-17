using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string middleName = "";
        private string fio;

        public string Firstname { get; set; }
        public string MiddleName { get; set; }

        public string Lastname { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string FIO 
        { 
            get
            {
                if (fio != null)
                {
                    return fio;
                }
                else
                {
                    string result = Firstname;

                    if (!(middleName == null || middleName == ""))
                    {
                        result = result + " " + MiddleName;
                    }

                    result = result + " " + Lastname;

                    return result;
                }
            }
            set
            {
                fio = value;
            }
        }
        public string WorkPhone { get; set; }
        public string AllPhones 
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) 
                        + "\r\n" + CleanUp(MobilePhone) 
                        + "\r\n" + CleanUp(WorkPhone)).Trim();
                }
            }

            set
            {
                allPhones = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, " -()", "\r\n");
                //phone.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "") + "\r\n";
        }

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
