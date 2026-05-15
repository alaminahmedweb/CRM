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
        private readonly ItmpReceiveAndPaymentService _tmpRecieveAndPayment;
        private readonly ItmpDailySalesAndCollectionService _tmpDailySalesAndCollection;
        private readonly ItmpDailyReceiveAndPaymentService _tmpDailyRecieveAndPayment;
        private readonly ItmpProfitAndLossAccService _tmpProfitAndLossAccService;
        private readonly ItmpQueryHandelService _tmpQueryHandelService;
        private readonly IChartOfAccountService _chartOfAccountService;

        public AccountsReportController(IMISReportQueryService misReportQueryService,
            ItmpReceiveAndPaymentService tmpRecieveAndPayment,
            ItmpDailySalesAndCollectionService tmpDailySalesAndCollection,
            ItmpDailyReceiveAndPaymentService tmpDailyRecieveAndPayment,
            ItmpProfitAndLossAccService tmpProfitAndLossAccService,
            ItmpQueryHandelService tmpQueryHandelService,
            IChartOfAccountService chartOfAccountService
            )
        {
            this._misReportQueryService = misReportQueryService;
            this._tmpRecieveAndPayment = tmpRecieveAndPayment;
            this._tmpDailySalesAndCollection = tmpDailySalesAndCollection;
            this._tmpDailyRecieveAndPayment =tmpDailyRecieveAndPayment;
            this._tmpProfitAndLossAccService =tmpProfitAndLossAccService;
            this._tmpQueryHandelService = tmpQueryHandelService;
            this._chartOfAccountService = chartOfAccountService;
        }

        public IActionResult Index()
        {
            ViewBag.CodeList = _chartOfAccountService.Find(a=>a.Id!=0);
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

        public async Task<IActionResult> ShowDailyReceiveAndPayment(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Daily Receive And Payment Statement";
            ViewBag.PageSize = "Legal";
            var msg = await _misReportQueryService.GetDailyReceiveAndPaymentReport(model.DateFrom, model.DateTo);
            var returnData = await _tmpDailyRecieveAndPayment.GetAllAsync();
            List<tmpDailyReceiveAndPayment> data = new List<tmpDailyReceiveAndPayment>();
            foreach (var item in returnData)
            {
                tmpDailyReceiveAndPayment dataItem = new tmpDailyReceiveAndPayment();
                dataItem.SlNo = item.SlNo;
                dataItem.SLNo2 = item.SLNo2;
                dataItem.Code = item.Code;
                dataItem.Code2 = item.Code2;
                dataItem.Particulars = item.Particulars;
                dataItem.Particulars2 = item.Particulars2;
                dataItem.Cash = item.Cash;
                dataItem.Cash2 = item.Cash2;
                dataItem.Bank = item.Bank;
                dataItem.Bank2 = item.Bank2;
                dataItem.Total = item.Total;
                dataItem.Total2 = item.Total2;

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
            var msg = await _misReportQueryService.GetDailySalesAndCollection(model.DateFrom, model.DateTo);
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


        public async Task<IActionResult> ShowProfitAndLossAcc(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Profit And Loss Statement";
            ViewBag.PageSize = "Legal";
            var msg = await _misReportQueryService.GetProfitAndLossAcc(model.DateFrom, model.DateTo);
            var returnData = await _tmpProfitAndLossAccService.GetAllAsync();
            List<tmpProfitAndLossAcc> data = new List<tmpProfitAndLossAcc>();
            foreach (var item in returnData)
            {
                tmpProfitAndLossAcc dataItem = new tmpProfitAndLossAcc();
                dataItem.Code = item.Code;
                dataItem.Description = item.Description;
                dataItem.InnerAmt = item.InnerAmt;
                dataItem.TotalAmt = item.TotalAmt;
                dataItem.GroupStatus = item.GroupStatus;
                dataItem.GId = item.GId;
                dataItem.GName = item.GName;

                data.Add(item);

            }

            return View(data);
        }

        public async Task<IActionResult> ShowLedgerQuery(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }
            string acHeadName = "";
            string acCode = "";
            ViewBag.DateRange = model;
            
            ViewBag.PageSize = "Legal";
            var msg = await _misReportQueryService.GetLedgerQuery(model.AcCode, model.DateFrom, model.DateTo);
            var returnData = await _tmpQueryHandelService.GetAllAsync();
            List<tmpQueryHandel> data = new List<tmpQueryHandel>();
            foreach (var item in returnData)
            {
                tmpQueryHandel dataItem = new tmpQueryHandel();
                dataItem.Code = item.Code;
                dataItem.TrNo = item.TrNo;
                dataItem.Row_Id = item.Row_Id;
                dataItem.Instrument = item.Instrument;
                dataItem.Remark = item.Remark;
                dataItem.Trans_dt = item.Trans_dt;
                dataItem.Debit = item.Debit;
                dataItem.Credit = item.Credit;
                dataItem.ContraDesc = item.ContraDesc;
                dataItem.VoucherType = item.VoucherType;
                dataItem.ACHead = item.ACHead;
                dataItem.opbal = item.opbal;
                dataItem.Remarks = item.Remarks;
                dataItem.ContraHead = item.ContraHead;
                acHeadName = item.ACHead;
                acCode = item.Code;
                data.Add(item);

            }
            ViewBag.ReportTitle = "Ledger Of ( "+ acCode +" ) "+ acHeadName;
            return View(data);
        }

        public async Task<IActionResult> ShowApprovedTransaction(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Approved Transaction List";
            ViewBag.PageSize = "Legal";
            var data =  _misReportQueryService.ShowApprovedTransaction(model.DateFrom, model.DateTo);
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
