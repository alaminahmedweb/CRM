using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            this._brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _brandService.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            await _brandService.AddEntity(brand);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await _brandService.GetByIdAsync(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Brand brand)
        {
            await _brandService.UpdateEntity(brand);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _brandService.GetByIdAsync(id);
            return View(data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            await _brandService.DeleteEntity(id);
            return RedirectToAction("Index");
        }
    }
}
