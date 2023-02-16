using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class FollowupController : Controller
    {
        private readonly IFollowupService _followupService;
        private readonly IFollowupQueryService _followupQueryService;
        private readonly IMapper _mapper;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IMonthListService _monthListService;

        public FollowupController(IFollowupService followupService,
            IFollowupQueryService followupQueryService,
            IMapper mapper            ,
            IServiceTypeService serviceTypeService,
            IMonthListService monthListService
            )
        {
            this._followupService = followupService;
            this._followupQueryService = followupQueryService;
            this._mapper = mapper;
            this._serviceTypeService = serviceTypeService;
            this._monthListService = monthListService;

        }
        public IActionResult Index()
        {
            IEnumerable<CustomerDto> followupList = _followupQueryService.GetDailyFollowupList(DateTime.Now);
            IEnumerable<DailyFollowupListVM> Viewresult = _mapper.Map<IEnumerable<CustomerDto>,
                IEnumerable<DailyFollowupListVM>>(followupList);
            return View(Viewresult);
        }

        public IActionResult GetFollowupsByCustId(int id)
        {
            FollowupDetailsByIdDto followupList = _followupQueryService.GetFollowupDetailsByCustId(id);
            return View(followupList);
        }

        public async Task<IActionResult> AddNewFollowup(int id)
        {
            CustomerFollowupDto customerDto = _followupQueryService.GetCustomerDetailsByCustId(id);
            AddFollowupVM Viewresult = _mapper.Map<CustomerFollowupDto,
                AddFollowupVM>(customerDto);
            ViewBag.ServiceList = await _serviceTypeService.GetAllAsync();
            ViewBag.MonthList = await _monthListService.GetAllAsync();

            return View(Viewresult);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewFollowup(AddFollowupVM addFollowupVM)
        {
            try
            {
                var followup = _mapper.Map<AddFollowupVM, Followup>(addFollowupVM);
                if (followup != null)
                {
                    int followupId = await _followupService.AddEntity(followup);
                    if(followup.Status=="Confirmed")
                    {
                        return RedirectToAction("Index", "Booking",new { id = followupId });
                    }
                    else
                    {
                        IEnumerable<CustomerDto> followupList = _followupQueryService.GetDailyFollowupList(DateTime.Now);
                        IEnumerable<DailyFollowupListVM> Viewresult = _mapper.Map<IEnumerable<CustomerDto>, 
                            IEnumerable<DailyFollowupListVM>>(followupList);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
       }

    }
}
