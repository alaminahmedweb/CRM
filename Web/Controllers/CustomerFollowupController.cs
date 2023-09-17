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
    public class CustomerFollowupController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IEmployeeService _employeeService;
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
        private readonly ICustomerQueryService _customerQueryService;
        private readonly ICategoryService _categoryService;

        public CustomerFollowupController(IContactByService contactByService,
            IEmployeeService employeeService,
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
            ICustomerFollowupService customerFollowupService,
            ICustomerQueryService customerQueryService,
            ICategoryService categoryService)
        {
            this._employeeService = employeeService;
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
            this._customerQueryService = customerQueryService;
            this._categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CustomerFollowupVM customerFollowupVM = new CustomerFollowupVM();
            customerFollowupVM.ServiceList =  _serviceTypeService.Find(a => a.Status == "Active");
            customerFollowupVM.DesignationList = await _designationService.GetAllAsync();
            customerFollowupVM.CityList = await _cityService.GetAllAsync();
            customerFollowupVM.ContactList =  _contactByService.Find(a => a.Status == "Active");
            customerFollowupVM.EmployeeList =  _employeeService.Find(a => a.Status == "Active");
            customerFollowupVM.MonthList = await _monthListService.GetAllAsync();
            customerFollowupVM.BrandList = await _brandService.GetAllAsync();
            customerFollowupVM.CategoryList = await _categoryService.GetAllAsync();
            return View(customerFollowupVM);
        }

        [HttpPost]
        public async Task<ActionResult> Save(CustomerFollowupMasterVM customerFollowupVM)
        {
            if (customerFollowupVM.BuildingDetails == null)
            {
                ModelState.AddModelError("", "Building Details Please");
            }
            if (customerFollowupVM.ContractDetails == null)
            {
                ModelState.AddModelError("", "Contract Details Please");
            }
            if (ModelState.IsValid)
            {
                CustomerFollowupDto customerFollowupDto = new CustomerFollowupDto();
                customerFollowupDto.Name = customerFollowupVM.CustomerFollowup.Name;
                customerFollowupDto.Address = customerFollowupVM.CustomerFollowup.Address;
                customerFollowupDto.SubAreaId = customerFollowupVM.CustomerFollowup.SubAreaId;
                customerFollowupDto.NoOfFloor = customerFollowupVM.CustomerFollowup.NoOfFloor;
                customerFollowupDto.NoOfFlat = customerFollowupVM.CustomerFollowup.NoOfFlat;
                customerFollowupDto.ContactId = customerFollowupVM.CustomerFollowup.ContactId;
                customerFollowupDto.EmployeeId = customerFollowupVM.CustomerFollowup.EmployeeId;
                customerFollowupDto.ModifiedBy = customerFollowupVM.CustomerFollowup.ModifiedBy;

                customerFollowupDto.CallingDate = customerFollowupVM.CustomerFollowup.CallingDate;
                customerFollowupDto.OfferAmount = customerFollowupVM.CustomerFollowup.OfferAmount;
                customerFollowupDto.AgreeAmount = customerFollowupVM.CustomerFollowup.AgreeAmount;
                customerFollowupDto.CustomerDoTheWorkingMonth = customerFollowupVM.CustomerFollowup.CustomerDoTheWorkingMonth;
                customerFollowupDto.Remarks = customerFollowupVM.CustomerFollowup.Remarks==null ? "": customerFollowupVM.CustomerFollowup.Remarks;
                customerFollowupDto.PositiveOrNegative = customerFollowupVM.CustomerFollowup.PositiveOrNegative;
                customerFollowupDto.DiscussionDetailsNote = customerFollowupVM.CustomerFollowup.DiscussionDetailsNote;
                customerFollowupDto.MarketingNextPlan = customerFollowupVM.CustomerFollowup.MarketingNextPlan == null ? "":customerFollowupVM.CustomerFollowup.MarketingNextPlan;
                customerFollowupDto.FollowupCallDate = customerFollowupVM.CustomerFollowup.FollowupCallDate;
                customerFollowupDto.Status = customerFollowupVM.CustomerFollowup.Status;
                customerFollowupDto.ServiceTypeId = customerFollowupVM.CustomerFollowup.ServiceTypeId;
                customerFollowupDto.CategoryId = customerFollowupVM.CustomerFollowup.CategoryId;
                customerFollowupDto.ReferenceBy = customerFollowupVM.CustomerFollowup.ReferenceBy;

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

                int followupId = await _customerFollowupService.AddEntity(customerFollowupDto);
                if (followupId != 0)
                {
                    if (customerFollowupDto.Status == "Confirmed")
                    {
                        TempData["SuccessMessage"] = "Saved Successfully..";
                        return Json(new { redirecturl = "/Booking/Index?followupId=" + followupId });
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Saved Successfully..";
                        return Json(new { redirecturl = "/CustomerFollowup/Index/" });
                    }
                }
            }
            return BadRequest(ModelState);
        }

        [HttpGet]
        public JsonResult GetAreaByCityId(int id)
        {
            var data =  _areaService.Find(a => a.CityId == id);
            return Json(data);
        }

        [HttpGet]
        public JsonResult GetSubAreaByAreaId(int id)
        {
            var data =  _subAreaService.Find(a => a.AreaId == id);
            return Json(data);
        }

        [HttpGet]
        public JsonResult IsMobileNoAlreadyExists(string mobileNo)
        {
            bool isMobileNoAlreadyExists = _customerQueryService.IsMobileNoAlreadyExists(mobileNo,0);
            return Json(isMobileNoAlreadyExists);
        }
    }
}
