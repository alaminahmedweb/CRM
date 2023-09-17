using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Create(ContactBy model)
        {
            if (await _contactService.IsRecordExistsAsync(a => a.Name == model.Name))
            {
                ModelState.AddModelError("", "Contact Name Already Exists..Please Check");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                int id = await _contactService.AddEntity(model);
                if (id != 0)
                {
                    TempData["SuccessMessage"] = "Created Successfully..";
                    return RedirectToAction("Create");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var _data = await _contactService.GetByIdAsync(id);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Update")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> UpdateContact(ContactBy model)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _contactService.UpdateEntity(model);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Updated Successfully..";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var _data = await _contactService.GetByIdAsync(id);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var isSuccess = await _contactService.DeleteEntity(id);
            if (isSuccess == true)
            {
                TempData["SuccessMessage"] = "Deleted Successfully..";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<JsonResult> GetList(string param)
        {
            var data = await _contactService.GetAllAsync();
            var jsonResult = Json(new { data });
            return jsonResult;
        }

    }
}
