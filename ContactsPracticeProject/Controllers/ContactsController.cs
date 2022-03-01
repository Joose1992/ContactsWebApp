using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice.DataAccess;
using Practice.DataAccess.Controllers;
using ContactsPracticeProject.Models;

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

        [HttpGet]
        public  async Task<IActionResult> Details()
        {
           if (ModelState.IsValid)
            {
                return View();
            }
           return View();
        }

        [HttpPost]
        public IActionResult Index(int contactId, string firstName, string lastName, string phoneNumber, string emailAddress, string address)
        {
            Details();

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

