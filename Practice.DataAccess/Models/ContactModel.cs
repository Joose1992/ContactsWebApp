using System;
namespace Practice.DataAccess.Models
{
	public class ContactModel
	{
		public int ContactId { get; set; }
		public string FirstName { get; set; } = "";
		public string LastName { get; set; } = "";
		public string PhoneNumber { get; set; } = "";
		public string EmailAddress { get; set; } = "";
		public string Address { get; set; } = "";
	}
}

