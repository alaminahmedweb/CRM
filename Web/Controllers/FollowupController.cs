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
    public class FollowupController : Controller
    {
        private readonly IAddNewFollowupService _followupService;
        private readonly IFollowupQueryService _followupQueryService;
        private readonly IMapper _mapper;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IMonthListService _monthListService;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<FollowupController> _logger;

        public FollowupController(IAddNewFollowupService followupService,
            IFollowupQueryService followupQueryService,
            IMapper mapper,
            IServiceTypeService serviceTypeService,
            IMonthListService monthListService,
            IEmployeeService employeeService,
            ILogger<FollowupController> logger
            )
        {
            this._followupService = followupService;
            this._followupQueryService = followupQueryService;
            this._mapper = mapper;
            this._serviceTypeService = serviceTypeService;
            this._monthListService = monthListService;
            this._employeeService = employeeService;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.employeeService =await _employeeService.GetAllAsync();
            return View();
        }

        [HttpGet]
        public IActionResult GetFollowupsByCustId(int customerId, int followupId)
        {
            FollowupDetailsByIdDto followupList =  _followupQueryService.GetFollowupDetailsByCustId(customerId);
            if(followupId==0)
            {
                followupList.FollowupId = _followupQueryService.GetMaxFollowupIdByCustId(customerId);
            }
            else
            {
                followupList.FollowupId = followupId;
            }
            return View(followupList);
        }

        [HttpGet]
        public async Task<IActionResult> AddNewFollowup(int followupId)
        {
            CustomerFollowupDto customerDto =  _followupQueryService.GetCustomerDetailsByFollowupId(followupId);
            AddFollowupVM Viewresult = _mapper.Map<CustomerFollowupDto, AddFollowupVM>(customerDto);

            Viewresult.ServiceList =  _serviceTypeService.Find(a => a.Status == "Active");
            Viewresult.MonthList = await _monthListService.GetAllAsync();
            Viewresult.EmployeeList =  _employeeService.Find(a => a.Status == "Active");

            return View(Viewresult);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFollowup(AddFollowupVM model)
        {
            if (ModelState.IsValid)
            {
                if (await _followupQueryService.IsAlreadyGivenFollowup(model.CustomerId, model.FollowupCallDate))
                {
                    ModelState.AddModelError("", "Followup already given on that date for this customer..");
                    model.ServiceList =  _serviceTypeService.Find(a => a.Status == "Active");
                    model.MonthList = await _monthListService.GetAllAsync();
                    model.EmployeeList =  _employeeService.Find(a => a.Status == "Active");
                    return View(model);
                }

                var followupDto = _mapper.Map<AddFollowupVM, AddFollowupDto>(model);
                followupDto.Remarks = followupDto.Remarks == null ? "" : followupDto.Remarks;
                followupDto.MarketingNextPlan = followupDto.MarketingNextPlan == null ? "" : followupDto.MarketingNextPlan;

                int followupId = await _followupService.AddEntity(followupDto);

                if (followupId != 0)
                {
                    if (followupDto.Status == "Confirmed")
                    {
                        TempData["SuccessMessage"] = "Added Followup Successfully..";
                        return RedirectToAction("Index", "Booking", new { followupId = followupId });
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Added Followup Successfully..";
                        IEnumerable<CustomerDto> followupList = _followupQueryService.GetDailyFollowupListByMpoIdAndFollowupType(DateTime.Now, DateTime.Now,-1,"");
                        IEnumerable<DailyFollowupListVM> Viewresult = _mapper.Map<IEnumerable<CustomerDto>,
                            IEnumerable<DailyFollowupListVM>>(followupList);
                        return RedirectToAction("Index");
                    }
                }
            }
            model.ServiceList =  _serviceTypeService.Find(a => a.Status == "Active");
            model.MonthList = await _monthListService.GetAllAsync();
            model.EmployeeList =  _employeeService.Find(a => a.Status == "Active");
            return View(model);
        }

        [HttpGet]
        public JsonResult GetDailyFollowupListByMpoIdAndFollowupType(DateTime dateFrom, DateTime dateTo, int mpoId, string followupType)
        {
            var data = _followupQueryService.GetDailyFollowupListByMpoIdAndFollowupType(dateFrom, dateTo, mpoId, followupType);
            var jsonResult = Json(new { data });
            return jsonResult;
        }
        //public async Task<JsonResult> GetList(string param)
        //{
        //    var data = await _serviceTypeService.GetAllAsync();
        //    var jsonResult = Json(new { data });
        //    //var jsonResult = JsonConvert.SerializeObject(data, Formatting.Indented);
        //    return jsonResult;
        //}

    }
}
