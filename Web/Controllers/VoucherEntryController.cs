using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class VoucherEntryController : Controller
    {

        private readonly IChartOfAccountService _chartOfAccountService;
        private readonly ITransactService _transactionService;

        public VoucherEntryController(IChartOfAccountService chartOfAccountService,
            ITransactService transactionService)
        {
            this._chartOfAccountService = chartOfAccountService;
            this._transactionService = transactionService;
        }

        public IActionResult Index()
        {
            ViewBag.ChartOfAccountList = _chartOfAccountService.Find(a=> ! a.Code.EndsWith("0000"));
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Save(VoucherEntryVM voucherEntryVM)
        {
            if (voucherEntryVM.VoucherEntryDetailsVM == null)
            {
                ModelState.AddModelError("", "Voucher Details Please");
            }

            if (ModelState.IsValid)
            {
                TransactDto transactionDto = new TransactDto();

                foreach (var item in voucherEntryVM.VoucherEntryDetailsVM)
                {
                    TransactDetailsDto transactionDetails = new TransactDetailsDto();
                    transactionDetails.Code = item.AcCode;
                    transactionDetails.Description = item.AcDesc;
                    transactionDetails.Narration = item.Narration;
                    transactionDetails.Debit = item.Debit;
                    transactionDetails.Credit = item.Credit;
                    transactionDetails.VoucherNo = voucherEntryVM.VoucherEntryMasterVM.VoucherNo;
                    transactionDetails.Remarks = voucherEntryVM.VoucherEntryMasterVM.Remarks;
                    transactionDetails.VoucherType = voucherEntryVM.VoucherEntryMasterVM.VoucherType;
                    transactionDetails.ModifiedBy = voucherEntryVM.VoucherEntryMasterVM.ModifiedBy;
                    transactionDto.TransactionDetails.Add(transactionDetails);
                }
                int transactionId = await _transactionService.AddEntity(transactionDto);
                if (transactionId != 0)
                {
                    TempData["SuccessMessage"] = "Saved Successfully..";
                    return Json(new { redirecturl = "/VoucherEntry/Index/" });
                }
                //int followupId = await _customerFollowupService.AddEntity(customerFollowupDto);
                //if (followupId != 0)
                //{
                //    if (customerFollowupDto.Status == "Confirmed")
                //    {
                //        TempData["SuccessMessage"] = "Saved Successfully..";
                //        return Json(new { redirecturl = "/Booking/Index?followupId=" + followupId });
                //    }
                //    else
                //    {
                //        TempData["SuccessMessage"] = "Saved Successfully..";
                //        return Json(new { redirecturl = "/CustomerFollowup/Index/" });
                //    }
                //}
            }
            return BadRequest(ModelState);
        }

        public async Task<JsonResult> GetList(string param)
        {
            try
            {
                var data = await _chartOfAccountService.GetAllAsync();
                var jsonResult = Json(new { data });
                return jsonResult;
            }
            catch (Exception ex)
            {
                return Json(new { });
            }
        }

    }
}
