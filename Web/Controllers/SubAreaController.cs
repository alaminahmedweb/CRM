using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
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
            try
            {
                if (ModelState.IsValid)
                {
                    int id = await _subAreaService.AddEntity(model);
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
            var _data = await _subAreaService.GetByIdAsync(id);
            ViewBag.AreaList = await _areaService.GetAllAsync();
            return View(_data);
        }

        [HttpPost]
        [ActionName("Update")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> UpdateMpo(SubArea model)
        {
            try
            {
                if(ModelState.IsValid)
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
            catch(Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
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
            try
            {
                var isSuccess = await _subAreaService.DeleteEntity(id);
                if (isSuccess == true)
                {
                    TempData["SuccessMessage"] = "Deleted Successfully..";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "Sub Area can't be delete because some records are in database need to be deleted first....";
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
