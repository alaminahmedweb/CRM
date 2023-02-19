using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ServiceFeedbackController : Controller
    {
        private readonly IFeedbackQueryService _feedbackQueryService;
        private readonly IMapper _mapper;
        private readonly IFeedbackService _feedbackService;

        public ServiceFeedbackController(IFeedbackQueryService feedbackQueryService,
            IMapper mapper,
            IFeedbackService feedbackService
            )
        {
            this._feedbackQueryService = feedbackQueryService;
            this._mapper = mapper;
            this._feedbackService = feedbackService;
        }
        public IActionResult Index()
        {
            var bookingInfo= _feedbackQueryService.GetBookingList(DateTime.Now);
            var bookingInfoVM= _mapper.Map<List<ServiceFeedbackDto>, List<ServiceFeedbackVM>>(bookingInfo);
            return View(bookingInfoVM);
        }
        public IActionResult AddFeedback(int BookingId)
        {
            var bookingInfo = _feedbackQueryService.GetBookingListByid(BookingId);
            var bookingInfoVM = _mapper.Map<ServiceFeedbackDto, ServiceFeedbackVM>(bookingInfo);
            return View(bookingInfoVM);
        }
        [HttpPost]
        public IActionResult AddFeedback(ServiceFeedback serviceFeedback)
        {
            try
            {
                _feedbackService.AddEntity(serviceFeedback);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
