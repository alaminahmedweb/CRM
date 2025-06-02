using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Infrastructure.Data.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingQueryService _bookingQueryService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        private readonly ITeamService _teamService;
        private readonly IEmployeeService _employeeService;
        private readonly IFollowupQueryService _followupQueryService;
        private readonly IFollowupService _followupService;
        private readonly IUnitOfWok _unitOfWok;
        private readonly IMISReportQueryService _misReportQueryService;

        public BookingController(IBookingQueryService bookingQueryService,
            IBookingService bookingService,
            IMapper mapper,
            ITeamService teamService,
            IEmployeeService employeeService,
            IUnitOfWok unitOfWok,
            IFollowupQueryService followupQueryService,
            IFollowupService followupService,
            IMISReportQueryService misReportQueryService)
        {
            this._bookingQueryService = bookingQueryService;
            this._bookingService = bookingService;
            this._mapper = mapper;
            this._teamService = teamService;
            this._employeeService = employeeService;
            this._followupQueryService = followupQueryService;
            this._followupService = followupService;
            this._unitOfWok = unitOfWok;
            this._misReportQueryService = misReportQueryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int followupId)
        {
            CustomerFollowupDto bookingDetails = _followupQueryService.GetCustomerDetailsByFollowupId(followupId);
            BookingVM bookingInfo = _mapper.Map<CustomerFollowupDto, BookingVM>(bookingDetails);
            ViewBag.TeamInfo = _teamService.Find(a => a.Status == "Active");
            ViewBag.EmployeeList = _employeeService.Find(a => a.Status == "Active");
            return View(bookingInfo);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBooking(Booking model)
        {
            if (_bookingQueryService.IsBookedAlready(model.TeamId, model.ShiftId, model.BookingDate))
            {
                ModelState.AddModelError("", "Already Booked for this date,team and shift");
                TempData["ErrorMessage"] = "Already Booked..";
            }
            if (_bookingQueryService.IsPendingBookedAlready(model.TeamId, model.ShiftId, model.BookingDate))
            {
                ModelState.AddModelError("", "Already Pending Booked for this date,team and shift");
                TempData["ErrorMessage"] = "Booking Shift Pending In This Slot..Please Approve First";
            }

            if (ModelState.IsValid)
            {
                model.PaymentDate=model.BookingDate;
                int id = await _bookingService.AddEntity(model);
                if (id != 0)
                {
                    TempData["SuccessMessage"] = "Booked Successfully..";
                    return RedirectToAction("Index", "Booking", new { followupId = model.FollowupId });
                }
            }
            return RedirectToAction("Index", new { followupId = model.FollowupId });
        }

        [HttpGet]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {

            BookingDto booking = _bookingQueryService.GetCustomerAndBookingDetailsByBookingId(bookingId);
            BookingVM bookingInfo = _mapper.Map<BookingDto, BookingVM>(booking);
            ViewBag.TeamInfo = _teamService.Find(a => a.Status == "Active");
            ViewBag.EmployeeList = _employeeService.Find(a => a.Status == "Active");
            return View(bookingInfo);
        }

        [HttpPost]
        public async Task<IActionResult> CancelBooking(Booking model)
        {
            if (ModelState.IsValid)
            {
                model.Status = "Cancel-Pending";
                bool isSuccess = await _bookingService.UpdateEntity(model);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Cancel Request Sent Successfully..";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ShiftBooking(int bookingId)
        {

            BookingDto booking = _bookingQueryService.GetCustomerAndBookingDetailsByBookingId(bookingId);
            BookingVM bookingInfo = _mapper.Map<BookingDto, BookingVM>(booking);
            ViewBag.TeamInfo = _teamService.Find(a => a.Status == "Active");
            ViewBag.EmployeeList = _employeeService.Find(a => a.Status == "Active");
            return View(bookingInfo);
        }

        [HttpPost]
        public async Task<IActionResult> ShiftBooking(Booking model)
        {
            if (_bookingQueryService.IsBookedAlready(model.TeamId, model.ShiftId, model.BookingDate))
            {
                ModelState.AddModelError("", "Already Booked for this date,team and shift");
                TempData["ErrorMessage"] = "Already Booked..";
            }
            if (_bookingQueryService.IsPendingBookedAlready(model.TeamId, model.ShiftId, model.BookingDate))
            {
                ModelState.AddModelError("", "Already Pending Booked for this date,team and shift");
                TempData["ErrorMessage"] = "Already Booked..";
            }

            if (ModelState.IsValid)
            {
                var modelToUpdate =await _bookingService.GetByIdAsync(model.Id);
                modelToUpdate.PendingShiftDate = model.BookingDate;
                modelToUpdate.PendingEntryDate = model.EntryDate;
                modelToUpdate.PendingTeamId = model.TeamId;
                modelToUpdate.PendingShiftId = model.ShiftId;
                modelToUpdate.PendingBookingById = model.BookingById;
                modelToUpdate.PendingBookingNote = model.BookingNote;
                modelToUpdate.Status = model.Status;

                //model.PaymentDate = model.BookingDate;
                bool isSuccess = await _bookingService.UpdateEntity(modelToUpdate);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Shift Request Sent Successfully..";
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("ShiftBooking", new { bookingId = model.Id });
        }

        [HttpGet]
        public IActionResult ShowBookingInfo()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ShowBookingData(DateTime dateFrom, DateTime dateTo)
        {
            var bookingItems = _bookingQueryService.GetBookingDetailsByDate(dateFrom.Date, dateTo.Date);
            return Json(bookingItems);
        }

        [HttpGet]
        public IActionResult ShowDueBookingInfo()
        {
            return View();
        }
        [HttpGet]
        public JsonResult ShowDueBookingData(DateTime dateFrom, DateTime dateTo)
        {
            var bookingItems = _bookingQueryService.GetDueBookingDetailsByDate(dateFrom.Date, dateTo.Date);
            return Json(bookingItems);
        }


        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> UpdateBookingAmount(int followupId)
        {
            Followup fol = await _followupService.GetByIdAsync(followupId);
            UpdateBookingAmountVM updateBookingAmountVM = new UpdateBookingAmountVM();
            updateBookingAmountVM.AgreeAmount = fol.AgreeAmount;
            updateBookingAmountVM.Remarks = fol.Remarks;

            //IEnumerable<Booking> booking = _bookingService.Find(a=>a.FollowupId== followupId);
            //foreach (Booking bookingItem in booking)
            //{
            //    updateBookingAmountVM.PaymentStatus = bookingItem.PaymentStatus;
            //}

            return View(updateBookingAmountVM);
        }

        [HttpPost]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> UpdateBookingAmount(UpdateBookingAmountVM model)
        {
            if (ModelState.IsValid)
            {
                _unitOfWok.BeginTransaction();
                try
                {
                    var entityToUpdate = await _followupService.GetByIdAsync(model.FollowupId);
                    entityToUpdate.PendingAgreeAmount = model.AgreeAmount;
                    entityToUpdate.ModifiedBy = model.ModifiedBy;
                    entityToUpdate.Remarks = model.Remarks;

                    bool isSuccess = await _followupService.UpdateEntity(entityToUpdate);

                    _unitOfWok.CommitTransaction();

                    if (isSuccess)
                    {
                        TempData["SuccessMessage"] = "Updated Successfully..";
                        return RedirectToAction("UpdateBookingAmount", new { followupId = model.FollowupId });
                    }
                }
                catch (Exception ex)
                {
                    _unitOfWok.RollbackTransaction();
                }
            }
            return RedirectToAction("UpdateBookingAmount", new { followupId = model.FollowupId });
        }

        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> ShowAllPendingBookingAmtUpdateData()
        {
            return View();
        }

        public JsonResult GetAllPendingBookingAmtUpdateData()
        {
            DateTime dateFrom = DateTime.ParseExact("2001-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime dateTo = DateTime.ParseExact("2050-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var data = _bookingQueryService.GetPendingBookingAmountChangeList(dateFrom, dateTo);
            return Json(data);
        }

        public async Task<JsonResult> ApproveBookingAmountChange(int followupId)
        {
            var entityToUpdate = await _followupService.GetByIdAsync(followupId);
            entityToUpdate.AgreeAmount = entityToUpdate.PendingAgreeAmount;
            entityToUpdate.PendingAgreeAmount = 0;
            bool isSuccess = await _followupService.UpdateEntity(entityToUpdate);
            return Json("Approved");
        }

        public async Task<JsonResult> MakeDueForBooking(int followupId)
        {
            bool isBookingUpdateSuccess = true;

            IEnumerable<Booking> bookingItems
                = _bookingService.Find(a => a.FollowupId == followupId);

            List<int> bookingList = new List<int>();
            int bookingId = 1;
            foreach (Booking data in bookingItems)
            {
                bookingId = data.Id;
                bookingList.Add(bookingId);
            }
            foreach (var item in bookingList)
            {
                Booking booking = await _bookingService.GetByIdAsync(item);
                booking.PaymentStatus = "Due";
                isBookingUpdateSuccess = await _bookingService.UpdateEntity(booking);
            }
            return Json("Successfully Changed Into Due..");
        }

        public async Task<JsonResult> MakePaidForBooking(int followupId)
        {
            bool isBookingUpdateSuccess = true;

            IEnumerable<Booking> bookingItems
                = _bookingService.Find(a => a.FollowupId == followupId);

            List<int> bookingList = new List<int>();
            int bookingId = 1;
            foreach (Booking data in bookingItems)
            {
                bookingId = data.Id;
                bookingList.Add(bookingId);
            }
            foreach (var item in bookingList)
            {
                Booking booking = await _bookingService.GetByIdAsync(item);
                booking.PaymentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
                booking.PaymentStatus = "Paid";
                isBookingUpdateSuccess = await _bookingService.UpdateEntity(booking);
            }
            return Json("Successfully Changed Into Paid...");
        }


        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> ShowAllPendingBookingCancelData()
        {
            return View();
        }

        public JsonResult GetAllPendingBookingCancelData()
        {
            DateTime dateFrom = DateTime.ParseExact("2001-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime dateTo = DateTime.ParseExact("2050-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var data = _misReportQueryService.GetBookingCancelAndShiftList(dateFrom, dateTo, "Cancel-Pending");
            return Json(data);
        }

        public async Task<JsonResult> ApprovePendingBookingCancelData(int bookingId)
        {
            Booking model= await _bookingService.GetByIdAsync(bookingId);
            model.Status = "Cancel";
            bool isSuccess = await _bookingService.UpdateEntity(model);
            return Json("Approved");
        }

        [HttpGet]
        [Authorize(Roles = "Super Admin,Admin")]
        public async Task<IActionResult> ShowAllPendingBookingShiftData()
        {
            return View();
        }

        public JsonResult GetAllPendingBookingShiftData()
        {
            DateTime dateFrom = DateTime.ParseExact("2001-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime dateTo = DateTime.ParseExact("2050-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var data = _misReportQueryService.GetPendingBookingShiftList(dateFrom, dateTo, "Shift-Pending");
            return Json(data);
        }

        public async Task<JsonResult> ApprovePendingBookingShiftData(int bookingId)
        {
            Booking model = await _bookingService.GetByIdAsync(bookingId);
            model.BookingDate =model.PendingShiftDate;
            model.EntryDate= model.PendingEntryDate;
            model.TeamId = model.PendingTeamId ;
            model.ShiftId= model.PendingShiftId ;
            model.BookingById= model.PendingBookingById ;
            model.BookingNote= model.PendingBookingNote ;
            model.Status = "Shifted";
            model.PaymentDate = model.PendingShiftDate;
            bool isSuccess = await _bookingService.UpdateEntity(model);
            return Json("Approved");
        }
    }
}
