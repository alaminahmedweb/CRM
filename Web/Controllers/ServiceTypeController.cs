using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Web.Controllers
{
    [Authorize]
    public class ServiceTypeController : Controller
    {
        private readonly IServiceTypeService _serviceTypeService;
        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            this._serviceTypeService = serviceTypeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //var data = await _serviceTypeService.GetAllAsync();
            return View();
        }

        public async Task<JsonResult> GetList(string param)
        {
            var data = await _serviceTypeService.GetAllAsync();
            var jsonResult = Json(new { data });
            //var jsonResult = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonResult;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceType model)
        {
            if (await _serviceTypeService.IsRecordExistsAsync(a => a.Name == model.Name))
            {
                ModelState.AddModelError("", "Service Already Exists..Please Check");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                int id = await _serviceTypeService.AddEntity(model);
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
            var data = await _serviceTypeService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> Update(ServiceType model)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _serviceTypeService.UpdateEntity(model);
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
            var data = await _serviceTypeService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            bool isSuccess = await _serviceTypeService.DeleteEntity(id);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Deleted Successfully..";
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
