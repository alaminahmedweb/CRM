using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingQueryService _bookingQueryService;
        private readonly IBookingService _bookingService;
        private readonly IMapper mapper;
        private readonly ITeamService _teamService;

        public BookingController(IBookingQueryService bookingQueryService,
            IBookingService bookingService,
            IMapper mapper,
            ITeamService teamService)
        {
            this._bookingQueryService = bookingQueryService;
            this._bookingService = bookingService;
            this.mapper = mapper;
            this._teamService = teamService;
        }

        public async Task<IActionResult> Index(int id)
        {
            BookingDto bookingDetails = _bookingQueryService.GetFollowupDetailsById(id);
            BookingVM bookingInfo = mapper.Map<BookingDto, BookingVM>(bookingDetails);
            ViewBag.BookingItems = _bookingQueryService.GetBookingDetailsByDate(DateTime.Now).ToList();
            ViewBag.TeamInfo = _teamService.Find(a => a.Status == "Active");
            return View(bookingInfo);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBooking(Booking booking)
        {
            int id = await _bookingService.AddEntity(booking);
            
            BookingDto bookingDetails = _bookingQueryService.GetFollowupDetailsById(id);
            BookingVM bookingInfo = mapper.Map<BookingDto, BookingVM>(bookingDetails);
            ViewBag.BookingItems = _bookingQueryService.GetBookingDetailsByDate(DateTime.Now).ToList();
            ViewBag.TeamInfo = _teamService.Find(a => a.Status == "Active");
            return View(bookingInfo);
        }
        public IActionResult ShowBookingInfo(int id)
        {
            ViewBag.BookingItems = _bookingQueryService.GetBookingDetailsByDate(DateTime.Now).ToList();
            return View();
        }

        public JsonResult ShowBookingData(DateTime date)
        {
            var bookingItems = _bookingQueryService.GetBookingDetailsByDate(date.Date).ToList();
            return Json(bookingItems);
        }

    }
}
