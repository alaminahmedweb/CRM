using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Authorize]
    public class SubAreaController : Controller
    {
        private readonly ISubAreaService _subAreaService;
        private readonly IAreaService _areaService;

        public SubAreaController(ISubAreaService subAreaService, IAreaService areaService)
        {
            this._subAreaService = subAreaService;
            this._areaService = areaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<SubArea> data = await _subAreaService.GetAllAsync();
            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.AreaList = await _areaService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubArea model)
        {
            if (await _subAreaService.IsRecordExistsAsync(a => a.Name == model.Name))
            {
                ModelState.AddModelError("", "Sub Area Name Already Exists..Please Check");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                int id = await _subAreaService.AddEntity(model);
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
            var _data = await _subAreaService.GetByIdAsync(id);
            ViewBag.AreaList = await _areaService.GetAllAsync();
            return View(_data);
        }

        [HttpPost]
        [ActionName("Update")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> UpdateMpo(SubArea model)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _subAreaService.UpdateEntity(model);
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
            var _data = await _subAreaService.GetByIdAsync(id);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var isSuccess = await _subAreaService.DeleteEntity(id);
            if (isSuccess == true)
            {
                TempData["SuccessMessage"] = "Deleted Successfully..";
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<JsonResult> GetList(string param)
        {
            var data = await _subAreaService.GetAllAsync();
            var jsonResult = Json(new { data });
            return jsonResult;
        }
    }
}
