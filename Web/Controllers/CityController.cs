using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Authorize]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            this._cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _cityService.GetAllAsync();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(City model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = await _cityService.AddEntity(model);
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
            var data = await _cityService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Update(City model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isSuccess = await _cityService.UpdateEntity(model);
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
            var data = await _cityService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin,Admin")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            try
            {
                var isSuccess = await _cityService.DeleteEntity(id);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Deleted Successfully..";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorMessage"] = "City can't be delete because some records are in database need to be deleted first....";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "City can't be delete because some records are in database need to be deleted first....";
                return RedirectToAction("Index");
            }

        }

    }
}
