using System;

namespace Practice.DataAccess.Models
{
	public class ContactModel
	{
        public ContactModel(int contactId, string firstName, string lastName, string phoneNumber, string emailAddress, string address)
        {
            ContactId = contactId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Address = address;
        }

        public ContactModel()
        {

        }


        public int ContactId { get; set; }

		public string FirstName { get; set; } = "";

		public string LastName { get; set; } = "";

		public string PhoneNumber { get; set; } = "";
		
		public string EmailAddress { get; set; } = "";

		public string Address { get; set; } = "";
	}
}

