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

        public FollowupController(IFollowupService followupService,
            IFollowupQueryService followupQueryService,
            IMapper mapper
            )
        {
            this._followupService = followupService;
            this._followupQueryService = followupQueryService;
            this._mapper = mapper;  
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

        public IActionResult AddNewFollowup(int id)
        {
            CustomerDto customerDto = _followupQueryService.GetCustomerDetailsByCustId(id);
            AddFollowupVM Viewresult = _mapper.Map<CustomerDto,
                AddFollowupVM>(customerDto);
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
