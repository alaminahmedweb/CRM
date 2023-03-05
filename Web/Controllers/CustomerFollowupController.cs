using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class CustomerFollowupController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _mpoService;
        private readonly IContactByService _contactByService;
        private readonly IAreaService _areaService;
        private readonly IFollowupService _followupService;
        private readonly IMapper _mapper;
        private readonly IDesignationService _designationService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IMonthListService _monthListService;
        private readonly ICityService _cityService;
        private readonly ISubAreaService _subAreaService;
        private readonly IBrandService _brandService;
        private readonly ICustomerFollowupService _customerFollowupService;

        public CustomerFollowupController(IContactByService contactByService,
            IEmployeeService mpoService,
            IAreaService areaService,
            IFollowupService followupService,
            ICustomerService customerService,
            IMapper mapper,
            IDesignationService designationService,
            IServiceTypeService serviceTypeService,
            ICityService cityService,
            ISubAreaService subAreaService,
            IMonthListService monthListService,
            IBrandService brandService,
            ICustomerFollowupService customerFollowupService
            )
        {
            this._mpoService = mpoService;
            this._contactByService = contactByService;
            this._areaService = areaService;
            this._followupService = followupService;
            this._mapper = mapper;
            this._designationService = designationService;
            this._serviceTypeService = serviceTypeService;
            this._cityService = cityService;
            this._subAreaService = subAreaService;
            this._monthListService = monthListService;
            this._brandService = brandService;
            this._customerService = customerService;
            this._customerFollowupService = customerFollowupService;
        }
        public async Task<IActionResult> Index()
        {

            ViewBag.ServiceList = await _serviceTypeService.GetAllAsync();
            ViewBag.DesignationList = await _designationService.GetAllAsync();
            ViewBag.CityList = await _cityService.GetAllAsync();
            ViewBag.ContactList = await _contactByService.GetAllAsync();
            ViewBag.MpoList = await _mpoService.GetAllAsync();
            ViewBag.MonthList=await _monthListService.GetAllAsync();
            ViewBag.BrandList=await _brandService.GetAllAsync();
            CustomerFollowupVM customerFollowupVM = new CustomerFollowupVM();
            return View(customerFollowupVM);
        }
        [HttpPost]
        public async Task<ActionResult> Save(CustomerFollowupMasterVM customerFollowupVM)
        {
            try
            {
                CustomerFollowupDto customerFollowupDto = new CustomerFollowupDto();
                customerFollowupDto.Name = customerFollowupVM.CustomerFollowup.Name;
                customerFollowupDto.Address = customerFollowupVM.CustomerFollowup.Address;
                customerFollowupDto.SubAreaId = customerFollowupVM.CustomerFollowup.SubAreaId;
                customerFollowupDto.NoOfFloor = customerFollowupVM.CustomerFollowup.NoOfFloor;
                customerFollowupDto.NoOfFlat = customerFollowupVM.CustomerFollowup.NoOfFlat;
                customerFollowupDto.ContactId = customerFollowupVM.CustomerFollowup.ContactId;
                customerFollowupDto.EmployeeId = customerFollowupVM.CustomerFollowup.EmployeeId;

                customerFollowupDto.CallingDate = customerFollowupVM.CustomerFollowup.CallingDate;
                customerFollowupDto.OfferAmount = customerFollowupVM.CustomerFollowup.OfferAmount;
                customerFollowupDto.AgreeAmount = customerFollowupVM.CustomerFollowup.AgreeAmount;
                customerFollowupDto.CustomerDoTheWorkingMonth = customerFollowupVM.CustomerFollowup.CustomerDoTheWorkingMonth;
                customerFollowupDto.Remarks = customerFollowupVM.CustomerFollowup.Remarks;
                customerFollowupDto.PositiveOrNegative = customerFollowupVM.CustomerFollowup.PositiveOrNegative;
                customerFollowupDto.DiscussionDetailsNote = customerFollowupVM.CustomerFollowup.DiscussionDetailsNote;
                customerFollowupDto.MarketingNextPlan = customerFollowupVM.CustomerFollowup.MarketingNextPlan;
                customerFollowupDto.FollowupCallDate = customerFollowupVM.CustomerFollowup.FollowupCallDate;
                customerFollowupDto.Status = customerFollowupVM.CustomerFollowup.Status;
                customerFollowupDto.ServiceTypeId = customerFollowupVM.CustomerFollowup.ServiceTypeId;

                foreach (var item in customerFollowupVM.BuildingDetails)
                {
                    BuildingDetailsDto buildingDetails = new BuildingDetailsDto();
                    buildingDetails.BrandId = item.BrandId;
                    buildingDetails.Quantity = item.Quantity;
                    buildingDetails.Capacity = item.Capacity;
                    customerFollowupDto.BuildingDetails.Add(buildingDetails);
                }
                foreach (var item in customerFollowupVM.ContractDetails)
                {
                    ContractDetailsDto contractDetails = new ContractDetailsDto();
                    contractDetails.Name = item.Name;
                    contractDetails.MobileNo = item.MobileNo;
                    contractDetails.DesignationId = item.DesignationId;
                    customerFollowupDto.ContractDetails.Add(contractDetails);
                }

                int followupId= await _customerFollowupService.AddEntity(customerFollowupDto);

                if (customerFollowupDto.Status == "Confirmed")
                {
                    return Json(new { redirecturl = "/Booking/Index/"+ followupId });
                }
                else
                {
                    ViewBag.ContactList = await _contactByService.GetAllAsync();
                    ViewBag.MpoList = await _mpoService.GetAllAsync();
                    ViewBag.AreaList = await _areaService.GetAllAsync();
                    ViewBag.MonthList = await _monthListService.GetAllAsync();
                    return Json(new { redirecturl = "/CustomerFollowup/Index/" });
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public JsonResult GetAreaByCityId(int id)
        {
            var data = _areaService.Find(a => a.CityId == id);
            return Json(data);
        }
        public JsonResult GetSubAreaByAreaId(int id)
        {
            var data = _subAreaService.Find(a=>a.AreaId== id);
            return Json(data);
        }
    }
}
