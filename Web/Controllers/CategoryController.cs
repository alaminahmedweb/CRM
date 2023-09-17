using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Web.Controllers
{
    [Authorize]

    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _categoryService.GetAllAsync();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category model)
        {
            if (await _categoryService.IsRecordExistsAsync(a => a.Name == model.Name))
            {
                ModelState.AddModelError("", "Category Name Already Exists..Please Check");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                int id = await _categoryService.AddEntity(model);
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
            var data = await _categoryService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Update(Category model)
        {
            if (ModelState.IsValid)
            {
                var isSuccess = await _categoryService.UpdateEntity(model);
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
            var data = await _categoryService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin,Admin")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            var isSuccess = await _categoryService.DeleteEntity(id);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Deleted Successfully..";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<JsonResult> GetList(string param)
        {
            var data = await _categoryService.GetAllAsync();
            var jsonResult = Json(new { data });
            return jsonResult;
        }
    }
}
