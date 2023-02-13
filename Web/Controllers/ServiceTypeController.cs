using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ServiceTypeController : Controller
    {
        private readonly IServiceTypeService _serviceTypeService;
        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            this._serviceTypeService = serviceTypeService;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _serviceTypeService.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceType serviceType)
        {
            await _serviceTypeService.AddEntity(serviceType);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var data = await _serviceTypeService.GetByIdAsync(id);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ServiceType serviceType)
        {
            await _serviceTypeService.UpdateEntity(serviceType);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _serviceTypeService.GetByIdAsync(id);
            return View(data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteEntity(int id)
        {
            await _serviceTypeService.DeleteEntity(id);
            return RedirectToAction("Index");
        }
    }
}
