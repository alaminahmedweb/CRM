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
    public class CustomerController : Controller
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
        private readonly IContractDetailsService _contractDetailsService;
        private readonly IBuildingDetailsService _buildingDetailsService;

        public CustomerController(IContactByService contactByService,
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
            ICategoryService categoryService,
            IContractDetailsService contractDetailsService,
            IBuildingDetailsService buildingDetailsService)
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
            this._contractDetailsService = contractDetailsService;
            this._buildingDetailsService = buildingDetailsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SearchCustomerVM customerFollowupVM = new SearchCustomerVM();
            customerFollowupVM.ServiceList = _serviceTypeService.Find(a => a.Status == "Active");
            customerFollowupVM.DesignationList = await _designationService.GetAllAsync();
            customerFollowupVM.CityList = await _cityService.GetAllAsync();
            customerFollowupVM.ContactList = _contactByService.Find(a => a.Status == "Active");
            customerFollowupVM.EmployeeList = _employeeService.Find(a => a.Status == "Active");
            customerFollowupVM.MonthList = await _monthListService.GetAllAsync();
            customerFollowupVM.BrandList = await _brandService.GetAllAsync();
            customerFollowupVM.CategoryList = await _categoryService.GetAllAsync();
            return View(customerFollowupVM);
        }

        [HttpGet]
        public JsonResult GetCustomerByMobileNo(string term)
        {
            var data = _customerQueryService.GetAllCustomersByMobileNo(term);
            return Json(data);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCustomerDetails(int customerId,int followupId)
        {
            CustomerDto customerDto = _customerQueryService.GetCustomerById(customerId);
            CustomerVM customerVM = _mapper.Map<CustomerDto, CustomerVM>(customerDto);
            customerVM.DesignationList = await _designationService.GetAllAsync();
            customerVM.CityList = await _cityService.GetAllAsync();
            customerVM.AreaList = await _areaService.GetAllAsync();
            customerVM.SubAreaList = await _subAreaService.GetAllAsync();
            customerVM.ContactList = _contactByService.Find(a => a.Status == "Active");
            customerVM.EmployeeList = _employeeService.Find(a => a.Status == "Active");
            customerVM.BrandList = await _brandService.GetAllAsync();
            customerVM.CategoryList = await _categoryService.GetAllAsync();
            ViewBag.FollowupId = followupId;
            return View(customerVM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomerDetails(CustomerVM model)
        {
            if (ModelState.IsValid)
            {
                var entityToUpdate = _mapper.Map<CustomerVM, Customer>(model);
                bool isSuccess = await _customerService.UpdateEntity(entityToUpdate);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Updated Successfully..";
                    return RedirectToAction("UpdateCustomerDetails", new { customerId = model.Id });
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNewContractDetails(int customerId)
        {
            ViewBag.DesignationList = await _designationService.GetAllAsync();
            ViewBag.CustomerId = customerId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewContractDetails(ContractDetails model)
        {
            if (IsMobileNoAlreadyExists(model.MobileNo, 0))
            {
                ModelState.AddModelError("", "Mobile No Already Exists..Please Check");
            }

            if (ModelState.IsValid)
            {
                int id = await _contractDetailsService.AddEntity(model);
                if (id != 0)
                {
                    TempData["SuccessMessage"] = "Saved Successfully..";
                    return RedirectToAction("UpdateCustomerDetails", new { customerId = model.CustomerId });
                }
            }
            ViewBag.DesignationList = await _designationService.GetAllAsync();
            ViewBag.CustomerId = model.CustomerId;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateContractDetails(int id)
        {
            ViewBag.DesignationList = await _designationService.GetAllAsync();
            var model = await _contractDetailsService.GetByIdAsync(id);
            ViewBag.CustomerId = id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateContractDetails(ContractDetails model)
        {
            if(await _contractDetailsService.IsRecordExistsAsync(a=>a.MobileNo==model.MobileNo && a.Id!=model.Id))
            {
                ModelState.AddModelError("", "Mobile No Already Exists..Please Check");
            }

            if (ModelState.IsValid)
            {
                bool isSuccess = await _contractDetailsService.UpdateEntity(model);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Updated Successfully..";
                    return RedirectToAction("UpdateCustomerDetails", new { customerId = model.CustomerId });
                }
            }
            ViewBag.DesignationList = await _designationService.GetAllAsync();
            ViewBag.CustomerId = model.CustomerId;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteContractDetails(int id)
        {
            ViewBag.DesignationList = await _designationService.GetAllAsync();
            var model = await _contractDetailsService.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContractDetails(ContractDetails model)
        {
            var noOfRecord = _contractDetailsService.Find(a => a.CustomerId == model.CustomerId).Count();
            if (noOfRecord == 1)
            {
                TempData["ErrorMessage"] = "At Least One Mobile No Should Exists..Can't Delete Record";
                return RedirectToAction("UpdateCustomerDetails", new { customerId = model.CustomerId });
            }

            bool isSuccess = await _contractDetailsService.DeleteEntity(model.Id);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Deleted Successfully..";
                return RedirectToAction("UpdateCustomerDetails", new { customerId = model.CustomerId });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddNewBuildingDetails(int customerId)
        {
            ViewBag.BrandList = await _brandService.GetAllAsync();
            ViewBag.CustomerId = customerId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBuildingDetails(BuildingDetails model)
        {

            if (await _buildingDetailsService.IsRecordExistsAsync(a => a.CustomerId == model.CustomerId && a.BrandId == model.BrandId))
            {
                ModelState.AddModelError("", "Brand Already Exists..Please Check");
            }

            if (ModelState.IsValid)
            {
                int id = await _buildingDetailsService.AddEntity(model);
                if (id != 0)
                {
                    TempData["SuccessMessage"] = "Saved Successfully..";
                    return RedirectToAction("UpdateCustomerDetails", new { customerId = model.CustomerId });
                }
            }
            ViewBag.BrandList = await _brandService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateBuildingDetails(int id)
        {
            ViewBag.BrandList = await _brandService.GetAllAsync();
            var model = await _buildingDetailsService.GetByIdAsync(id);
            ViewBag.CustomerId = id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBuildingDetails(BuildingDetails model)
        {
            if (await _buildingDetailsService.IsRecordExistsAsync(a => a.CustomerId == model.CustomerId && a.Id != model.Id && a.BrandId==model.BrandId))
            {
                ModelState.AddModelError("", "Brand Already Exists..Please Check");
            }
            if (ModelState.IsValid)
            {
                bool isSuccess = await _buildingDetailsService.UpdateEntity(model);
                if (isSuccess)
                {
                    TempData["SuccessMessage"] = "Updated Successfully..";
                    return RedirectToAction("UpdateCustomerDetails", new { customerId = model.CustomerId });
                }
            }
            ViewBag.CustomerId = model.CustomerId;
            ViewBag.BrandList = await _brandService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBuildingDetails(int id)
        {
            ViewBag.BrandList = await _brandService.GetAllAsync();
            var model = await _buildingDetailsService.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBuildingDetails(ContractDetails model)
        {
            var noOfRecord = _buildingDetailsService.Find(a => a.CustomerId == model.CustomerId).Count();
            if (noOfRecord == 1)
            {
                TempData["ErrorMessage"] = "At Least One Building Details Should Exists..Can't Delete Record";
                return RedirectToAction("UpdateCustomerDetails", new { customerId = model.CustomerId });
            }

            bool isSuccess = await _buildingDetailsService.DeleteEntity(model.Id);
            if (isSuccess)
            {
                TempData["SuccessMessage"] = "Deleted Successfully..";
                return RedirectToAction("UpdateCustomerDetails", new { customerId = model.CustomerId });
            }
            return View(model);
        }

        public bool IsMobileNoAlreadyExists(string mobileNo, int customerId)
        {
            return _customerQueryService.IsMobileNoAlreadyExists(mobileNo, customerId);
        }

    }
}
