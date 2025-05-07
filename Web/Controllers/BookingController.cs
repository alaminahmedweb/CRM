using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public BookingController(IBookingQueryService bookingQueryService,
            IBookingService bookingService,
            IMapper mapper,
            ITeamService teamService,
            IEmployeeService employeeService,
            IUnitOfWok unitOfWok,
            IFollowupQueryService followupQueryService,
            IFollowupService followupService)
        {
            this._bookingQueryService = bookingQueryService;
            this._bookingService = bookingService;
            this._mapper = mapper;
            this._teamService = teamService;
            this._employeeService = employeeService;
            this._followupQueryService = followupQueryService;
            this._followupService = followupService;
            this._unitOfWok = unitOfWok;

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
                model.Status = "Cancel";
                bool isSuccess = await _bookingService.UpdateEntity(model);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Canceled Successfully..";
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
        //[Bind("Id,TeamId,ShiftId,FollowupId,BookingDate,ModifiedBy")] 
        public async Task<IActionResult> ShiftBooking(Booking model)
        {
            if (_bookingQueryService.IsBookedAlready(model.TeamId, model.ShiftId, model.BookingDate))
            {
                ModelState.AddModelError("", "Already Booked for this date,team and shift");
                TempData["ErrorMessage"] = "Already Booked..";
            }
            if (ModelState.IsValid)
            {
                bool isSuccess = await _bookingService.UpdateEntity(model);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Shifted Successfully..";
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

            IEnumerable<Booking> booking = _bookingService.Find(a=>a.FollowupId== followupId);
            foreach (Booking bookingItem in booking)
            {
                updateBookingAmountVM.PaymentStatus = bookingItem.PaymentStatus;
            }

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
                    entityToUpdate.AgreeAmount = model.AgreeAmount;
                    entityToUpdate.ModifiedBy = model.ModifiedBy;
                    entityToUpdate.Remarks = model.Remarks;

                    bool isSuccess = await _followupService.UpdateEntity(entityToUpdate);
                    bool isBookingUpdateSuccess = true;

                    IEnumerable<Booking> bookingItems
                        = _bookingService.Find(a => a.FollowupId == model.FollowupId);

                    List<int> bookingList = new List<int>();
                    int bookingId = 1;
                    foreach (Booking data in bookingItems)
                    {
                        bookingId = data.Id;
                        bookingList.Add(bookingId);
                    }
                    foreach(var item in bookingList)
                    {
                        Booking booking = await _bookingService.GetByIdAsync(item);
                        if (booking.PaymentStatus == "Due" && model.PaymentStatus == "Paid")
                        {
                            booking.PaymentDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
                        }
                        booking.PaymentStatus = model.PaymentStatus;
                        isBookingUpdateSuccess = await _bookingService.UpdateEntity(booking);
                    }

                    _unitOfWok.CommitTransaction();
                    if (isSuccess && isBookingUpdateSuccess)
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
    }
}
