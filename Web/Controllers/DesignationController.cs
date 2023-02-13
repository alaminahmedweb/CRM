using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IDesignationService _designationService;

        public DesignationController(IDesignationService designationService)
        {
            this._designationService = designationService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _designationService.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Designation designation)
        {
            await _designationService.AddEntity(designation);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await _designationService.GetByIdAsync(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Designation designation)
        {
            await _designationService.UpdateEntity(designation);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _designationService.GetByIdAsync(id);
            return View(data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            await _designationService.DeleteEntity(id);
            return RedirectToAction("Index");
        }
    }
}
