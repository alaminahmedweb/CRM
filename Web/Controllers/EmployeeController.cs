using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;   
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Employee> listMpo = await _employeeService.GetAllAsync();
            return View(listMpo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            int id=await _employeeService.AddEntity(employee);
            if (id!=0)
            {
                return RedirectToAction("Index"); 
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var _data=await _employeeService.GetByIdAsync(id);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateMpo(Employee employee)
        {
            bool isSuccess=await _employeeService.UpdateEntity(employee);
            if(isSuccess)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var _data = await _employeeService.GetByIdAsync(id);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteMpo(int id)
        {
            var _data = await _employeeService.DeleteEntity(id);
            if(_data==true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
