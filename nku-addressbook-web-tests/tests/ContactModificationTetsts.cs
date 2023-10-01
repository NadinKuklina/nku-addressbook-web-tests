using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTetsts : AuthTestBase 
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("newFirstName", "newLastName");            

            app.Contacts.ModifyByIndex(1, newData);
        }
    }
}
