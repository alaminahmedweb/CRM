using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class SubAreaController : Controller
    {
        private readonly ISubAreaService _subAreaService;
        private readonly IAreaService _areaService;

        public SubAreaController(ISubAreaService subAreaService,IAreaService areaService)
        {
            this._subAreaService = subAreaService;
            this._areaService = areaService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<SubArea> data =await _subAreaService.GetAllAsync();
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.AreaList = await _areaService.GetAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubArea area)
        {
            int id = await _subAreaService.AddEntity(area);
            if (id != 0)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> Update(int id)
        {
            var _data = await _subAreaService.GetByIdAsync(id);
            ViewBag.AreaList = await _areaService.GetAllAsync();
            return View(_data);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdateMpo(SubArea area)
        {
            bool isSuccess = await _subAreaService.UpdateEntity(area);
            if (isSuccess)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var _data = await _subAreaService.GetByIdAsync(id);
            return View(_data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            var _data = await _subAreaService.DeleteEntity(id);
            if (_data == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
