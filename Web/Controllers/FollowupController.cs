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
        private readonly IFollowupService followupService;
        private readonly IFollowupQueryService followupQueryService;
        private readonly IMapper _mapper;

        public FollowupController(IFollowupService followupService,
            IFollowupQueryService followupQueryService,
            IMapper mapper
            )
        {
            this.followupService = followupService;
            this.followupQueryService = followupQueryService;
            this._mapper = mapper;  
        }
        public IActionResult Index()
        {
            IEnumerable<CustomerDto> followupList = followupQueryService.GetDailyFollowupList(DateTime.Now);
            IEnumerable<DailyFollowupListVM> Viewresult = _mapper.Map<IEnumerable<CustomerDto>,
                IEnumerable<DailyFollowupListVM>>(followupList);
            return View(Viewresult);
        }

        public IActionResult GetFollowupsByCustId(int id)
        {
            FollowupDetailsByIdDto followupList = followupQueryService.GetFollowupDetailsByCustId(id);
            return View(followupList);
        }

        public IActionResult AddNewFollowup(int id)
        {
            CustomerDto customerDto = followupQueryService.GetCustomerDetailsByCustId(id);
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
                    int followupId = await followupService.AddEntity(followup);
                    if(followup.Status=="Confirmed")
                    {
                        return RedirectToAction("Index", "Booking",new { id = followupId });
                    }
                    else
                    {
                        IEnumerable<CustomerDto> followupList = followupQueryService.GetDailyFollowupList(DateTime.Now);
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
