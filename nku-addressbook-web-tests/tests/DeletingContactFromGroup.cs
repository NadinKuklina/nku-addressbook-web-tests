using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class DeletingContactFromGroup : AuthTestBase
    {
        [Test]
        public void TestDeletingContactFromGroup()
        {
            //Arrange            
            app.Groups.Create(new GroupData("groupForTestDeleteFromGroup"));
            string idNewGroup = GroupData.LastAddedGroupId();
            GroupData group = GroupData.GetAll().FirstOrDefault(i => i.Id == idNewGroup);

            app.Contacts.CreateContact(new ContactData("NameOfUser", "LastNameOfUser"));
            string idNewContact = ContactData.LastAddedContactId();
            ContactData contact = ContactData.GetAll().FirstOrDefault(i => i.Id == idNewContact);

            app.Contacts.AddContactToGroup(contact, group);
            List<ContactData> oldList = group.GetContacts();

            //Act
            app.Contacts.DeleteContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();

            //Assert
            Assert.AreEqual(oldList, newList);
        }
    }
}
