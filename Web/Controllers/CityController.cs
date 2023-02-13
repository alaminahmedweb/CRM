using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            this._cityService = cityService;
        }
        public async Task<IActionResult> Index()
        {
            var data=await _cityService.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(City city)
        {
            await _cityService.AddEntity(city);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await _cityService.GetByIdAsync(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(City city)
        {
            await _cityService.UpdateEntity(city);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _cityService.GetByIdAsync(id);
            return View(data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            await _cityService.DeleteEntity(id);
            return RedirectToAction("Index");
        }

    }
}
