using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            this._brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _brandService.GetAllAsync();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int id = await _brandService.AddEntity(model);
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
            var data = await _brandService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Update(Brand model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isSuccess = await _brandService.UpdateEntity(model);
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
            var data = await _brandService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            try
            {
                var isSuccess = await _brandService.DeleteEntity(id);
                if (isSuccess)
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
