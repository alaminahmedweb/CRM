using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class AccountsReportController : Controller
    {
        private readonly IMISReportQueryService _misReportQueryService;
        private readonly ItmpReceiveAndPayment _tmpRecieveAndPayment;
        private readonly ItmpDailySalesAndCollection _tmpDailySalesAndCollection;
        public AccountsReportController(IMISReportQueryService misReportQueryService,
            ItmpReceiveAndPayment tmpRecieveAndPayment,
            ItmpDailySalesAndCollection tmpDailySalesAndCollection)
        {
            this._misReportQueryService = misReportQueryService;
            _tmpRecieveAndPayment = tmpRecieveAndPayment;
            _tmpDailySalesAndCollection = tmpDailySalesAndCollection;
        }

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetSuccessData()
        {
            return Json("success");
        }

        public async Task<IActionResult> ShowReceiveAndPayment(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Receive And Payment Statement";
            ViewBag.PageSize = "Legal";
            var msg =await _misReportQueryService.GetReceiveAndPaymentReport(model.DateFrom, model.DateTo);
            var returnData =await _tmpRecieveAndPayment.GetAllAsync();
            List<tmpReceiveAndPayment> data=new List<tmpReceiveAndPayment>();
            foreach (var item in returnData)
            {
                tmpReceiveAndPayment dataItem = new tmpReceiveAndPayment();
                dataItem.SlNo= item.SlNo;
                dataItem.Description = item.Description;
                dataItem.OpeningBalance = item.OpeningBalance;
                dataItem.Receive = item.Receive;
                dataItem.Payment = item.Payment;
                dataItem.ClosingBalance = item.ClosingBalance;

                data.Add(item);

            }

            return View(data);
        }

        public async Task<IActionResult> ShowDailySalesAndCollection(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Daily Sales And Collection Statement";
            ViewBag.PageSize = "Legal";
            var msg = await _misReportQueryService.GetReceiveAndPaymentReport(model.DateFrom, model.DateTo);
            var returnData = await _tmpDailySalesAndCollection.GetAllAsync();
            List<tmpDailySalesAndCollection> data = new List<tmpDailySalesAndCollection>();
            foreach (var item in returnData)
            {
                tmpDailySalesAndCollection dataItem = new tmpDailySalesAndCollection();
                dataItem.TrDate = item.TrDate;
                dataItem.Sales = item.Sales;
                dataItem.Collection = item.Collection;
                dataItem.DueCollection = item.DueCollection;
                dataItem.Due = item.Due;
                dataItem.NetCollection = item.NetCollection;

                data.Add(item);

            }

            return View(data);
        }
        public bool CheckUserDateSelectAuthority(DateTime DateFrom, DateTime DateTo)
        {
            if (User.IsInRole("Super Admin") || User.IsInRole("Admin") || User.IsInRole("Accounts"))
            {
                return true;
            }
            else
            {
                int dateDiff = (DateTo.Date - DateFrom.Date).Days;
                if (dateDiff > 3)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
