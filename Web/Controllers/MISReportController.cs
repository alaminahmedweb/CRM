using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Drawing;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class MISReportController : Controller
    {
        private readonly IMISReportQueryService _misReportQueryService;
        private readonly IEmployeeService _employeeService;
        private readonly IContactByService _contactByService;
        private readonly ICategoryService _categoryService;


        public MISReportController(IMISReportQueryService misReportQueryService,
            IEmployeeService employeeService,
            IContactByService contactByService,
            ICategoryService categoryService)
        {
            this._misReportQueryService = misReportQueryService;
            this._employeeService = employeeService;
            this._contactByService = contactByService;
            this._categoryService = categoryService;
        }
        public  IActionResult Index()
        {
            ViewBag.EmployeeList=_employeeService.Find(a=>a.Status=="Active");
            ViewBag.ContactList = _contactByService.Find(a => a.Name != "");
            ViewBag.CategoryList = _categoryService.Find(a => a.Name != "");
            return View();
        }

        public JsonResult GetAllCustomersByDate(DateTime dateFrom,DateTime dateTo,int employeeId,int contactId, int categoryId)
        {
            return Json(_misReportQueryService.GetAllCustomersByDate(dateFrom, dateTo, employeeId,contactId,categoryId));
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
            if(!CheckUserDateSelectAuthority(model.DateFrom,model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Status Wise Customer List";
            ViewBag.PageSize = "A4";
            var data = _misReportQueryService.GetStatusWiseCustomersListByDate(model.DateFrom, model.DateTo);
            return View(data);
            
        }
        public IActionResult ContactWiseNewCustomerList(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Contact Wise Customer List";
            ViewBag.PageSize = "A4";
            var data = _misReportQueryService.GetContactWiseCustomersListByDate(model.DateFrom, model.DateTo);
            return View(data);

        }
        public IActionResult StatusWiseFollowupDoneList(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Status Wise Followup Done List";
            ViewBag.PageSize = "A4";
            var data = _misReportQueryService.GetStatusWiseFollowupDoneListByDate(model.DateFrom, model.DateTo);
            return View(data);

        }
        public IActionResult ShowFollowupDoneList(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Followup Done List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetAllFollowupDoneListByDate(model.DateFrom, model.DateTo);
            return View(data);
        }
        public IActionResult ShowEntryDateWiseBookingList(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Entry Date Wise Booking List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetEntryDateWiseBookingList(model.DateFrom, model.DateTo);
            return View(data);
        }
        public IActionResult ShowBookingDateWiseBookingList(DateRangeVM model, string exportType = "")
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Booking Date Wise Booking List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetBookingDateWiseBookingList(model.DateFrom, model.DateTo);
            if (!string.IsNullOrEmpty(exportType) && exportType.ToLower() == "excel")
            {
                return ExportToExcel(data);
            }
            return View(data);
        }

        private IActionResult ExportToExcel(List<BookingItemDto> data)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("BookingReport");

                // Add headers
                worksheet.Cells[1, 1].Value = "Entry Date";
                worksheet.Cells[1, 2].Value = "Working Date";
                worksheet.Cells[1, 3].Value = "Team";
                worksheet.Cells[1, 4].Value = "Shift";
                worksheet.Cells[1, 5].Value = "Status";
                worksheet.Cells[1, 6].Value = "Mobile No";
                worksheet.Cells[1, 7].Value = "Name";
                worksheet.Cells[1, 8].Value = "Address";
                worksheet.Cells[1, 9].Value = "Amount";
                worksheet.Cells[1, 10].Value = "Note";

                // Format header row
                using (var range = worksheet.Cells[1, 1, 1, 10])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                }

                // Add data rows
                int row = 2;
                double total = 0;

                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.BookingEntryDate;
                    worksheet.Cells[row, 2].Value = item.BookingWorkingDate;
                    worksheet.Cells[row, 3].Value = item.TeamName;
                    worksheet.Cells[row, 4].Value = item.ShiftName;
                    worksheet.Cells[row, 5].Value = item.Status;
                    worksheet.Cells[row, 6].Value = item.MobileNo;
                    worksheet.Cells[row, 7].Value = item.Name;
                    worksheet.Cells[row, 8].Value = item.Address;
                    worksheet.Cells[row, 9].Value = item.AgreeAmount;
                    worksheet.Cells[row, 10].Value = item.BookingNote;

                    total = total + item.AgreeAmount;
                    row++;
                }

                // Add total row
                worksheet.Cells[row, 8].Value = "Total";
                worksheet.Cells[row, 9].Value = total;

                // Format total row
                using (var range = worksheet.Cells[row, 8, row, 9])
                {
                    range.Style.Font.Bold = true;
                }

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Generate file name with dates
                var fileName = $"BookingReport_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                // Return the Excel file
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        public IActionResult ShowBookingShiftingList(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Booking Shifting List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetBookingCancelAndShiftList(model.DateFrom, model.DateTo, "Shifted");
            return View(data);
        }
        public IActionResult ShowBookingCancelList(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Booking Cancel List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetBookingCancelAndShiftList(model.DateFrom, model.DateTo, "Cancel");
            return View(data);
        }
        public IActionResult ShowDueBookingList(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Due Booking List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetDueBookingList(model.DateFrom, model.DateTo);
            return View(data);
        }
        public IActionResult ShowCollectionReport(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "Collection Report";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetCollectionReport(model.DateFrom, model.DateTo);
            return View(data);
        }
        public IActionResult ShowNewCustomerList(DateRangeVM model)
        {
            if (!CheckUserDateSelectAuthority(model.DateFrom, model.DateTo))
            {
                return RedirectToAction("HttpStatusCodeHandler", "Error", new { statusCode = "401" });
            }

            ViewBag.DateRange = model;
            ViewBag.ReportTitle = "New Customer List";
            ViewBag.PageSize = "Legal";
            var data = _misReportQueryService.GetAllCustomersByDate(model.DateFrom, model.DateTo,model.EmployeeId,model.ContactId,model.CategoryId);
            return View(data);
        }
        public bool CheckUserDateSelectAuthority(DateTime DateFrom, DateTime DateTo)
        {
            if(User.IsInRole("Super Admin") || User.IsInRole("Admin"))
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
