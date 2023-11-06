using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            app.Groups.Create(new GroupData("groupForTestAddingToGroup"));
            string idNewGroup = GroupData.LastAddedGroupId();
            GroupData group = GroupData.GetAll().FirstOrDefault(i => i.Id == idNewGroup);

            app.Contacts.CreateContact(new ContactData("NameOfUserForAddingTest", "LastNameOfUserForAddingTest"));
            string idNewContact = ContactData.LastAddedContactId();
            ContactData contact = ContactData.GetAll().FirstOrDefault(i => i.Id == idNewContact);
            List<ContactData> oldList = group.GetContacts();

            //actions
            app.Contacts.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
