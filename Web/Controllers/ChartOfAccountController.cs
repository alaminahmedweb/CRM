using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

namespace Web.Controllers
{
    public class ChartOfAccountController : Controller
    {
        private readonly IChartOfAccountService _chartOfAccountService;

        public ChartOfAccountController(IChartOfAccountService chartOfAccountService)
        {
            this._chartOfAccountService = chartOfAccountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChartOfAccount model)
        {
            if (await _chartOfAccountService.IsRecordExistsAsync(a => a.Code == model.Code))
            {
                ModelState.AddModelError("", "Code Already Exists..Please Check");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                int id = await _chartOfAccountService.AddEntity(model);
                if (id != 0)
                {
                    TempData["SuccessMessage"] = "Created Successfully..";
                    return RedirectToAction("Create");
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin,Accounts")]
        public async Task<IActionResult> Update(int id)
        {
            var _data = await _chartOfAccountService.GetByIdAsync(id);
            ViewBag.CityList = _chartOfAccountService.Find(a => a.Id != 0);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Update")]
        [Authorize(Roles = "Super Admin,Admin,Accounts")]
        public async Task<IActionResult> UpdateChartOfAccount(ChartOfAccount model)
        {
            if (ModelState.IsValid)
            {
                bool isSuccess = await _chartOfAccountService.UpdateEntity(model);
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
            var data = await _chartOfAccountService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin,Admin")]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            var isSuccess = await _chartOfAccountService.DeleteEntity(id);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Deleted Successfully..";
                return RedirectToAction("Index");
            }
            return View();

        }


        public async Task<JsonResult> GetList(string param)
        {
            try
            {
                var data = await _chartOfAccountService.GetAllAsync();
                var jsonResult = Json(new { data });
                return jsonResult;
            }
            catch(Exception ex)
            {
                return Json(new {  });
            }
        }

    }
}
