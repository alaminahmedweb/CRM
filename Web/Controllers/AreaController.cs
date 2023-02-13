using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AreaController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly ICityService _cityService;

        public AreaController(IAreaService areaService,ICityService cityService)
        {
            this._areaService = areaService;
            this._cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Area> listArea = await _areaService.GetAllAsync();
            return View(listArea);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.CityList = await _cityService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Area area)
        {
            int id= await _areaService.AddEntity(area);
            if (id!=0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var _data = await _areaService.GetByIdAsync(id);
            ViewBag.CityList = await _cityService.GetAllAsync();
            return View(_data);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateMpo(Area area)
        {
            bool isSuccess = await _areaService.UpdateEntity(area);
            if (isSuccess)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var _data = await _areaService.GetByIdAsync(id);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var _data = await _areaService.DeleteEntity(id);
            if (_data == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
