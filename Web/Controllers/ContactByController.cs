using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ContactByController : Controller
    {
        private readonly IContactByService _contactService;
        public ContactByController(IContactByService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ContactBy> listContact = await _contactService.GetAllAsync();
            return View(listContact);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactBy contact)
        {
            int id = await _contactService.AddEntity(contact);
            if (id!=0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var _data = await _contactService.GetByIdAsync(id);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateContact(ContactBy contact)
        {
            bool isSuccess = await _contactService.UpdateEntity(contact);
            if (isSuccess)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var _data = await _contactService.GetByIdAsync(id);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var _data = await _contactService.DeleteEntity(id);
            if (_data == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
