using ApplicationCore.DtoModels;
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

        public ServiceFeedbackController(IFeedbackQueryService feedbackQueryService,
            IMapper mapper
            )
        {
            this._feedbackQueryService = feedbackQueryService;
            this._mapper = mapper;
        }
        public IActionResult Index()
        {
            var bookingInfo= _feedbackQueryService.GetBookingList(DateTime.Now);
            var bookingInfoVM= _mapper.Map<List<ServiceFeedbackDto>, List<ServiceFeedbackVM>>(bookingInfo);
            return View(bookingInfoVM);
        }
    }
}
