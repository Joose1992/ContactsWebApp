using Practice.DataAccess.Controllers;
using Practice.DataAccess.Models;
using Practice.DataAccess;

namespace ContactsPracticeProject.Models
{
    public class ContactsViewModel
    {
        private IPeopleInfoConfigManager _configuration;

        public List<ContactModel> ContactList { get; set; }

        public ContactModel CurrentContact { get; set; }

        public bool IsActionSuccess { get; set; }

        public string ActionMessage { get; set; }

        public ContactsViewModel(IPeopleInfoConfigManager configuration)
        {
            _configuration = configuration;
            ContactList = GetAllContacts();
            CurrentContact = ContactList.FirstOrDefault();
        }

        public ContactsViewModel(IPeopleInfoConfigManager configuration, int contactId)
        {
            _configuration = configuration;
            ContactList = new List<ContactModel>();

            if (contactId > 0)
            {
                CurrentContact = GetContact(contactId);
            }
            else
            {
                CurrentContact = new ContactModel();
            }
        }

        public List<ContactModel> GetAllContacts()
        {
            return ContactController.GetAllContacts(_configuration);
        }

        public ContactModel GetContact(int contactId)
        {
            return ContactController.GetContactByID(contactId, _configuration);
        }
    }
}
