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
        private string allPhones;        
        private string fi;
        private string allEmails;

        public string Firstname { get; set; }
        public string MiddleName { get; set; }

        public string Lastname { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string FI 
        { 
            get
            {
                if (!string.IsNullOrEmpty(fi))
                {
                    return fi;
                }
                else
                {        
                    string result = string.Empty;

                    if (!string.IsNullOrEmpty(Firstname))
                    {
                        result = Firstname;
                    }
                    if (!string.IsNullOrEmpty(Lastname))
                    {
                        if (string.IsNullOrEmpty(result))
                        {
                            result = Lastname;
                        }
                        else
                        {
                            result = result + " " + Lastname;
                        }
                    }
                    return result;
                }
            }
            set
            {
                fi = value;
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
                    string result = string.Empty;

                    if (!string.IsNullOrEmpty(HomePhone))
                    {
                        result = CleanUp(HomePhone);
                    }

                    if (!string.IsNullOrEmpty(MobilePhone))
                    {
                        if (string.IsNullOrEmpty(result))
                        {
                            result = CleanUp(MobilePhone);
                        }
                        else
                        {
                            result = result + "\r\n" + CleanUp(MobilePhone);
                        }
                    }


                    if (!string.IsNullOrEmpty(WorkPhone))
                    {
                        if (string.IsNullOrEmpty(result))
                        {
                            result = CleanUp(WorkPhone);
                        }
                        else
                        {
                            result = result + "\r\n" + CleanUp(WorkPhone);
                        }
                    }

                    return result.Trim();                    
                }
            }

            set
            {
                allPhones = value;
            }
        }
        public string Email1  { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    string result = string.Empty;

                    if (!string.IsNullOrEmpty(Email1))
                    {
                        result = Email1;
                    }

                    if (!string.IsNullOrEmpty(Email2))
                    {
                        if (string.IsNullOrEmpty(result))
                        {
                            result = Email2;
                        }
                        else
                        {
                            result = result + "\r\n" + Email2;
                        }                           
                    }
                    

                    if (!string.IsNullOrEmpty(Email3))
                    {
                        if (string.IsNullOrEmpty(result))
                        {
                            result = Email3;
                        }
                        else
                        {
                            result = result + "\r\n" + Email3;
                        }                        
                    }                      

                    return result;
                }
            }

            set
            {
                allEmails = value;
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

        public ContactData()
        {

        }

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
