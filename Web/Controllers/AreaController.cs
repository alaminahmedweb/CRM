using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
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
        public async Task<IActionResult> Create(Area model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = await _areaService.AddEntity(model);
                    if (id != 0)
                    {
                        TempData["SuccessMessage"] = "Created Successfully..";
                        return RedirectToAction("Index");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");

            }
        }

        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Update(int id)
        {
            var _data = await _areaService.GetByIdAsync(id);
            ViewBag.CityList = await _cityService.GetAllAsync();
            return View(_data);
        }

        [HttpPost]
        [ActionName("Update")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> UpdateMpo(Area model)
        {
            try
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
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
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
            try
            {
                var isSuccess = await _areaService.DeleteEntity(id);
                if (isSuccess == true)
                {
                    TempData["SuccessMessage"] = "Deleted Successfully..";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Area can't be delete because some records are in database need to be deleted first....";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

    }
}
