using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data.Queries;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AdvanceLedgerController : Controller
    {
        private readonly IDesignationService _designationService;
        private readonly IAdvanceLedgerService _advanceLedgerService;
        private readonly IAdvanceLedgerQueryService _advanceLedgerQueryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdvanceLedgerController(IDesignationService designationService,
            IAdvanceLedgerService advanceLedgerService,
            IAdvanceLedgerQueryService advanceLedgerQueryService,
            UserManager<ApplicationUser> userManager)
        {
            _designationService = designationService;
            _advanceLedgerService = advanceLedgerService;
            _advanceLedgerQueryService = advanceLedgerQueryService;
            this._userManager = userManager;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.DesignationList=await _designationService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdvanceLedger model)
        {
            if (ModelState.IsValid)
            {
                int MaxTrNo = _advanceLedgerQueryService.GetMaxTrNo();
                model.TrNo = MaxTrNo;
                int id = await _advanceLedgerService.AddEntity(model);
                if (id != 0)
                {
                    TempData["SuccessMessage"] = "Saved Successfully..Wait For Apporval";
                    return RedirectToAction("Create");
                }
            }
            ViewBag.DesignationList = _designationService.GetAllAsync();
            return View(model);
        }

        [Authorize(Roles = "Super Admin,Admin")]
        public IActionResult GetAllPendingAdvanceList()
        {
            return View();
        }

        public JsonResult GetAllPendingAdvanceEntry()
        {
            var data = _advanceLedgerQueryService.GetAllPendingAdvanceEntry();
            return Json(data);
        }

        public async Task<JsonResult> ApprovePendingAdvance(int id)
        {
            string userName = _userManager.GetUserId(User);

            AdvanceLedger model = await _advanceLedgerService.GetByIdAsync(id);
            model.IsApproved = 1;
            model.ApprovedBy = userName;
            model.ApprovedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time"); ;
            bool isSuccess = await _advanceLedgerService.UpdateEntity(model);
            return Json("Approved");
        }

        public async Task<JsonResult> DeclinePendingAdvance(int id)
        {
            string userName = _userManager.GetUserId(User);

            AdvanceLedger model = await _advanceLedgerService.GetByIdAsync(id);
            model.IsApproved = 2;
            model.ApprovedBy = userName;
            model.ApprovedDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time"); ;
            bool isSuccess = await _advanceLedgerService.UpdateEntity(model);
            return Json("Declined");
        }

        public async Task<IActionResult> GetApprovedDueAdvanceAmt()
        {
            return View();
        }

        public JsonResult GetAllUnAdjustedAdvanceAmt()
        {
            var data = _advanceLedgerQueryService.GetAllUnjustedAdvanceEntry();
            return Json(data);
        }

        public IActionResult AdjustAdvanceAmt(int trNo)
        {
            var data=_advanceLedgerQueryService.GetUnjustedAdvanceEntryByTrNo(trNo);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> AdjustAdvanceAmt(AdvanceLedger model)
        {
            var data = _advanceLedgerQueryService.GetUnjustedAdvanceEntryByTrNo(model.TrNo);
            data.AdvanceAmt = 0;
            data.IsApproved = 1;
            data.AdjustAmt = model.AdjustAmt;
            int id =await _advanceLedgerService.AddEntity(data);
            if (id != 0)
            {
                TempData["SuccessMessage"] = "Saved Successfully..";
                return RedirectToAction("GetApprovedDueAdvanceAmt");
            }
            return View();
        }
    }
}
