using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [Authorize]
    public class ServiceFeedbackController : Controller
    {
        private readonly IFeedbackQueryService _feedbackQueryService;
        private readonly IMapper _mapper;
        private readonly IFeedbackService _feedbackService;
        private readonly IComplainRegisterService _complainRegisterService;
        private readonly IComplainFeedbackService _complainFeedbackService;
        private readonly IAddComplainFeebackService _addComplainFeebackService;

        public ServiceFeedbackController(IFeedbackQueryService feedbackQueryService,
            IMapper mapper,
            IFeedbackService feedbackService,
            IComplainRegisterService complainRegisterService,
            IComplainFeedbackService complainFeedbackService,
            IAddComplainFeebackService addComplainFeebackService
            )
        {
            this._feedbackQueryService = feedbackQueryService;
            this._mapper = mapper;
            this._feedbackService = feedbackService;
            this._complainRegisterService = complainRegisterService;
            this._complainFeedbackService = complainFeedbackService;
            this._addComplainFeebackService = addComplainFeebackService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var bookingInfo = _feedbackQueryService.GetBookingList(DateTime.Now, DateTime.Now);
            return View(bookingInfo);
        }

        [HttpGet]
        public IActionResult AddFeedback(int bookingId)
        {
            var bookingInfo = _feedbackQueryService.GetBookingListByid(bookingId);
            return View(bookingInfo);
        }

        [HttpPost]
        public async Task<IActionResult> AddFeedback(ServiceFeedback model)
        {
            if (_feedbackQueryService.IsAlreadyGivenFeedback(model.BookingId))
            {
                TempData["ErrorMessage"] = "Already Given Feedback!!!";
                return RedirectToAction("Index", "Home");
            }
            int id = await _feedbackService.AddEntity(model);
            if (id != 0)
            {
                TempData["SuccessMessage"] = "Added Feedback Successfully..";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddComplain(int bookingId)
        {
            var bookingInfo = _feedbackQueryService.GetBookingListByid(bookingId);
            return View(bookingInfo);
        }

        [HttpPost]
        public async Task<IActionResult> AddComplain(ComplainRegister model)
        {
            //if (_feedbackQueryService.IsAlreadyGivenComplain(model.BookingId))
            //{
            //    TempData["ErrorMessage"] = "Already Given Complain!!!";
            //    return RedirectToAction("Index");
            //}

            int id = await _complainRegisterService.AddEntity(model);

            if (id != 0)
            {
                TempData["SuccessMessage"] = "Added Complain Successfully..";
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public IActionResult DailyComplainList()
        {
            var complainInfo = _feedbackQueryService.GetDailyComplainList(DateTime.Now, DateTime.Now);
            return View(complainInfo);
        }

        [HttpGet]
        public IActionResult AddComplainFeedback(int complainId)
        {
            var complainInfo = _feedbackQueryService.GetComplainDetailsById(complainId);
            return View(complainInfo);
        }

        [HttpPost]
        public async Task<IActionResult> AddComplainFeedback(ComplainFeedbackDto model)
        {
            if (_feedbackQueryService.IsAlreadyGivenComplainFeedback(model.ComplainId))
            {
                TempData["ErrorMessage"] = "Already Given Complain Feedback!!!";
                return RedirectToAction("Index", "Home");
            }
            int id = await _addComplainFeebackService.AddEntity(model);
            if (id != 0)
            {
                TempData["SuccessMessage"] = "Added Complain Feedback Successfully..";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public  JsonResult GetDailyBookingList(DateTime dateFrom, DateTime dateTo)
        {
            //var bookingInfo = _feedbackQueryService.GetBookingList(dateFrom, dateTo);
            //return Json(bookingInfo);
            var data = _feedbackQueryService.GetBookingList(dateFrom, dateTo);
            var jsonResult = Json(new { data });
            return jsonResult;
        }

        public JsonResult DailyComplainListByDate(DateTime dateFrom, DateTime dateTo)
        {
            //var complainInfo = _feedbackQueryService.GetDailyComplainList(dateFrom, dateTo);
            //return Json(complainInfo);
            var data = _feedbackQueryService.GetDailyComplainList(dateFrom, dateTo);
            var jsonResult = Json(new { data });
            return jsonResult;
        }
    }
}
