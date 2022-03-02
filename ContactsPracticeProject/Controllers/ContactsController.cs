using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Practice.DataAccess;
using Practice.DataAccess.Controllers;
using Practice.DataAccess.Models;
using ContactsPracticeProject.Models;
using System.ComponentModel.DataAnnotations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContactsPracticeProject.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IPeopleInfoConfigManager _configuration;

        public ContactsController(IPeopleInfoConfigManager configManager)
        {
            _configuration = configManager;
        }

        public IActionResult Index()
        {
            ContactsViewModel model = new ContactsViewModel(_configuration);
            return View(model);
        }

        public bool Validation(ContactModel contactModel)
        {
            if (contactModel.FirstName == "" && contactModel.FirstName == null) { return false; }

            if (contactModel.LastName == "" && contactModel.LastName == null){ return false; }

            if (contactModel.PhoneNumber.Length <= 12 || contactModel.PhoneNumber == "" && contactModel.PhoneNumber == null){ return false; }

            Regex containNumbers = new Regex(@"[0-9]+");
            if (!containNumbers.IsMatch(contactModel.EmailAddress)){return false;};

            Regex containsLowerCase = new Regex(@"[a-z]+");
            if (!containsLowerCase.IsMatch(contactModel.EmailAddress)){return false;}

            Regex containsUpperCase = new Regex(@"[A-Z]+");
            if (!containsUpperCase.IsMatch(contactModel.EmailAddress)){return false;}

            Regex specialCharacters = new Regex(@"[!@#$%^&*]+");
            if (!specialCharacters.IsMatch(contactModel.EmailAddress)) { return false; }

            if (contactModel.Address == "" && contactModel.Address == null){ return false; }

            return true;
          
        }

        [HttpPost]
        public IActionResult Index(int contactId, string firstName, string lastName, string phoneNumber, string emailAddress, string address)
        {

            ContactModel modelToBeValidated = new ContactModel(contactId, firstName, lastName, phoneNumber, emailAddress, address);

            if (Validation(modelToBeValidated))
            {
                if (contactId > 0)
                {
                    ContactController.UpdateContact(contactId, firstName, lastName, phoneNumber, emailAddress, address, _configuration);
                }
                else
                {
                    ContactController.CreateContact(firstName, lastName, phoneNumber, emailAddress, address, _configuration);
                }


                ContactsViewModel model = new ContactsViewModel(_configuration);
                model.IsActionSuccess = true;
                model.ActionMessage = "Contact has been saved succesfully";

                return View(model);
            }

            return View("Index");


        }

        public IActionResult Update(int id)
        {
            ContactsViewModel model = new ContactsViewModel(_configuration, id);
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                ContactController.DeleteContact(id, _configuration);
            }

            ContactsViewModel model = new ContactsViewModel(_configuration);
            model.IsActionSuccess = true;
            model.ActionMessage = "Contact has been deleted successfully";
            return View("Index",model);
        }
    }
}

