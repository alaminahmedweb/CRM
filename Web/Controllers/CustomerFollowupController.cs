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
            IBrandService brandService
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
        public async Task<ActionResult> Index(CustomerFollowupVM customerFollowupVM)
        {
            try
            {
                ViewBag.ContactList = await _contactByService.GetAllAsync();
                ViewBag.MpoList = await _mpoService.GetAllAsync();
                ViewBag.AreaList = await _areaService.GetAllAsync();
                ViewBag.MonthList = await _monthListService.GetAllAsync();

                var customer = _mapper.Map<CustomerFollowupVM, Customer>(customerFollowupVM);//await _customerService.GetByIdAsync(customerFollowupVM.CustomerId);
                if (customer != null)
                {
                    int custId = await _customerService.AddEntity(customer);
                    Followup followup = _mapper.Map<CustomerFollowupVM, Followup>(customerFollowupVM);
                    followup.CustomerId = custId;
                    int followupId = await _followupService.AddEntity(followup);
                    if (followup.Status == "Confirmed")
                    {
                        return RedirectToAction("Index", "Booking", new { id = followupId });
                    }
                    else
                    {
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
