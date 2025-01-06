using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class DraftCustomerController : Controller
    {
        private readonly IDraftCustomerService _draftCustomerService;
        private readonly IContactByService _contactByService;
        private readonly IDraftCustomerQueryService _draftCustomerQueryService;

        public DraftCustomerController(IDraftCustomerService draftCustomerService,
            IContactByService contactByService,
            IDraftCustomerQueryService draftCustomerQueryService)
        {
            this._draftCustomerService = draftCustomerService;
            this._contactByService = contactByService;
            this._draftCustomerQueryService = draftCustomerQueryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            DraftCustomerVM draftCustomerVM = new DraftCustomerVM();
            draftCustomerVM.ContactList = _contactByService.Find(a => a.Status == "Active");
            return View(draftCustomerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DraftCustomerVM model)
        {
            if (ModelState.IsValid)
            {
                DraftCustomer draftCustomer = new DraftCustomer{
                    ClientName=model.ClientName,
                    ModifiedDate=model.ModifiedDate,
                    ModifiedBy=model.ModifiedBy,
                    MobileNo=model.MobileNo,
                    Remarks = model.Remarks,
                    NextFollowupDate = model.NextFollowupDate,
                    IsFollowupDone= model.IsFollowupDone,
                    ContactId= model.ContactId
                };
                int id = await _draftCustomerService.AddEntity(draftCustomer);
                if (id != 0)
                {
                    TempData["SuccessMessage"] = "Created Successfully..";
                    return RedirectToAction("Create");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int followupId)
        {
            var data = await _draftCustomerService.GetByIdAsync(followupId);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DraftCustomer model)
        {
            if (ModelState.IsValid)
            {
                model.IsFollowupDone = true;
                var isSuccess = await _draftCustomerService.UpdateEntity(model);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Removed Successfully..";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _draftCustomerService.GetByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            var isSuccess = await _draftCustomerService.DeleteEntity(id);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Deleted Successfully..";
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public JsonResult GetDailyDraftCustomerFollowupListByDate(DateTime dateFrom, DateTime dateTo)
        {
            //var data = _draftCustomerService.Find(a => a.NextFollowupDate.Date >= dateFrom.Date
            //                       && a.NextFollowupDate.Date <= dateTo
            //                       && a.IsFollowupDone == false);
            
            var data= _draftCustomerQueryService.GetDraftCustomer(dateFrom, dateTo);    
            var jsonResult = Json(new { data });
            //return Json(data);

            //var jsonResult = JsonConvert.SerializeObject(data, Formatting.Indented);
            return jsonResult;

        }
    }
}
