using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Authorize]
    public class AreaController : Controller
    {
        private readonly IAreaService _areaService;
        private readonly ICityService _cityService;

        public AreaController(IAreaService areaService, ICityService cityService)
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
            ViewBag.CityList = _cityService.Find(a => a.Id != 0);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Area model)
        {
            if (await _areaService.IsRecordExistsAsync(a => a.Name == model.Name))
            {
                ModelState.AddModelError("", "Area Name Already Exists..Please Check");
                ViewBag.CityList = await _cityService.GetAllAsync();
                return View(model);
            }
            if (ModelState.IsValid)
            {
                int id = await _areaService.AddEntity(model);
                if (id != 0)
                {
                    TempData["SuccessMessage"] = "Created Successfully..";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CityList = _cityService.Find(a => a.Id != 0);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var _data = await _areaService.GetByIdAsync(id);
            ViewBag.CityList = _cityService.Find(a=>a.Id!=0);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Update")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> UpdateMpo(Area model)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _areaService.UpdateEntity(model);
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
            var _data = await _areaService.GetByIdAsync(id);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var isSuccess = await _areaService.DeleteEntity(id);
            if (isSuccess == true)
            {
                TempData["SuccessMessage"] = "Deleted Successfully..";
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<JsonResult> GetList(string param)
        {
            var data = await _areaService.GetAllAsync();
            var jsonResult = Json(new { data });
            return jsonResult;
        }
    }
}
