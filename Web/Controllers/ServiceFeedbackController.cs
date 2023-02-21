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
        private readonly IComplainRegisterService _complainRegisterService;
        private readonly IComplainFeedbackService _complainFeedbackService;

        public ServiceFeedbackController(IFeedbackQueryService feedbackQueryService,
            IMapper mapper,
            IFeedbackService feedbackService,
            IComplainRegisterService complainRegisterService,
            IComplainFeedbackService complainFeedbackService
            )
        {
            this._feedbackQueryService = feedbackQueryService;
            this._mapper = mapper;
            this._feedbackService = feedbackService;
            this._complainRegisterService = complainRegisterService;
            this._complainFeedbackService = complainFeedbackService;
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
        public async Task<IActionResult> AddFeedback(ServiceFeedback serviceFeedback)
        {
            try
            {
                int id= await _feedbackService.AddEntity(serviceFeedback);
                if(id!=0)
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw new Exception();
            }
        }
        //
        public IActionResult AddComplain(int BookingId)
        {
            var bookingInfo = _feedbackQueryService.GetBookingListByid(BookingId);
            var bookingInfoVM = _mapper.Map<ServiceFeedbackDto, ComplainRegisterVM>(bookingInfo);
            return View(bookingInfoVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddComplain(ComplainRegister complainRegister)
        {
            try
            {
                int id= await _complainRegisterService.AddEntity(complainRegister);
                if(id!=0)
                {
                    return RedirectToAction("Index");
                }
                return View();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public IActionResult DailyComplainList()
        {
            var complainInfo = _feedbackQueryService.GetDailyComplainList(DateTime.Now);
            var complainInfoVM = _mapper.Map<List<ComplainFeedbackDto>, List<ComplainFeedbackVM>>(complainInfo);
            return View(complainInfoVM);
        }

        public IActionResult AddComplainFeedback(int ComplainId)
        {
            var complainInfo =_feedbackQueryService.GetComplainDetailsById(ComplainId);
            var complainInfoVM =_mapper.Map<ComplainFeedbackDto, ComplainFeedbackVM>(complainInfo);
            return View(complainInfoVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddComplainFeedback(ComplainFeedback complainFeedback)
        {
            try
            {
                int id = await _complainFeedbackService.AddEntity(complainFeedback);
                if (id != 0)
                {
                    return RedirectToAction("DailyComplainList");
                }
                return View();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw new Exception();
            }
        }

    }
}
