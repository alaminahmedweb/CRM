using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class DesignationController : Controller
    {
        private readonly IDesignationService _designationService;

        public DesignationController(IDesignationService designationService)
        {
            this._designationService = designationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _designationService.GetAllAsync();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Designation model)
        {
            if (ModelState.IsValid)
            {
                int id= await _designationService.AddEntity(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var data = await _designationService.GetByIdAsync(id);
            return View(data);
        }
        
        [HttpPost]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Update(Designation model)
        {
            await _designationService.UpdateEntity(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _designationService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            await _designationService.DeleteEntity(id);
            return RedirectToAction("Index");
        }
    }
}
