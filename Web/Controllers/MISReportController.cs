using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class MISReportController : Controller
    {
        private readonly IMISReportQueryService _misReportQueryService;
        private readonly IEmployeeService _employeeService;

        public MISReportController(IMISReportQueryService misReportQueryService,IEmployeeService employeeService)
        {
            this._misReportQueryService = misReportQueryService;
            this._employeeService = employeeService;
        }
        public  IActionResult Index()
        {
            ViewBag.EmployeeList=_employeeService.Find(a=>a.Status=="Active");
            return View();
        }

        public JsonResult GetAllCustomersByDate(DateTime dateFrom,DateTime dateTo,int EmployeeId)
        {
            return Json(_misReportQueryService.GetAllCustomersByDate(dateFrom, dateTo, EmployeeId));
        }
        public JsonResult GetAllFollowupDoneListByDate(DateTime dateFrom, DateTime dateTo)
        {
            return Json(_misReportQueryService.GetAllFollowupDoneListByDate(dateFrom, dateTo));
        }
        public JsonResult GetEntryDateWiseBookingList(DateTime dateFrom, DateTime dateTo)
        {
            return Json(_misReportQueryService.GetEntryDateWiseBookingList(dateFrom, dateTo));
        }
        public JsonResult GetBookingCancelList(DateTime dateFrom, DateTime dateTo)
        {
            return Json(_misReportQueryService.GetBookingCancelAndShiftList(dateFrom, dateTo,"Cancel"));
        }
        public JsonResult GetBookingShiftingList(DateTime dateFrom, DateTime dateTo)
        {
            return Json(_misReportQueryService.GetBookingCancelAndShiftList(dateFrom, dateTo,"Shifted"));
        }
        public JsonResult GetContactWiseCustomersListByDate(DateTime dateFrom, DateTime dateTo)
        {
            return Json(_misReportQueryService.GetContactWiseCustomersListByDate(dateFrom, dateTo));
        }
        public JsonResult GetStatusWiseCustomersListByDate(DateTime dateFrom, DateTime dateTo)
        {
            //return Json(_misReportQueryService.GetStatusWiseCustomersListByDate(dateFrom, dateTo));
            return Json(new { redirecturl = "/MISReport/StatusWiseNewCustomerList?dateFrom="+ dateFrom +"&dateTo="+ dateTo + ""});
        }
        public JsonResult GetStatusWiseFollowupDoneListByDate(DateTime dateFrom, DateTime dateTo)
        {
            return Json(_misReportQueryService.GetStatusWiseFollowupDoneListByDate(dateFrom, dateTo));
        }
        public JsonResult GetSuccessData()
        {
            return Json("success");
        }
        public IActionResult StatusWiseNewCustomerList(DateRangeVM model)
        {
            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Status Wise Customer List";
            ViewBag.PageSize = "A4";
            var data = _misReportQueryService.GetStatusWiseCustomersListByDate(model.DateFrom, model.DateTo);
            return View(data);
            
        }
        public IActionResult ContactWiseNewCustomerList(DateRangeVM model)
        {
            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Contact Wise Customer List";
            ViewBag.PageSize = "A4";
            var data = _misReportQueryService.GetContactWiseCustomersListByDate(model.DateFrom, model.DateTo);
            return View(data);

        }
        public IActionResult StatusWiseFollowupDoneList(DateRangeVM model)
        {
            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Status Wise Followup Done List";
            ViewBag.PageSize = "A4";
            var data = _misReportQueryService.GetStatusWiseFollowupDoneListByDate(model.DateFrom, model.DateTo);
            return View(data);

        }
        public IActionResult ShowFollowupDoneList(DateRangeVM model)
        {
            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Followup Done List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetAllFollowupDoneListByDate(model.DateFrom, model.DateTo);
            return View(data);
        }
        public IActionResult ShowEntryDateWiseBookingList(DateRangeVM model)
        {
            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Entry Date Wise Booking List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetEntryDateWiseBookingList(model.DateFrom, model.DateTo);
            return View(data);
        }
        public IActionResult ShowBookingShiftingList(DateRangeVM model)
        {
            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Booking Shifting List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetBookingCancelAndShiftList(model.DateFrom, model.DateTo, "Shifted");
            return View(data);
        }
        public IActionResult ShowBookingCancelList(DateRangeVM model)
        {
            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Booking Cancel List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetBookingCancelAndShiftList(model.DateFrom, model.DateTo, "Cancel");
            return View(data);
        }
        public IActionResult ShowNewCustomerList(DateRangeVM model)
        {
            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "New Customer List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetAllCustomersByDate(model.DateFrom, model.DateTo,model.EmployeeId);
            return View(data);
        }

    }
}
