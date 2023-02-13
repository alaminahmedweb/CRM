using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamService teamService;
        private readonly IEmployeeService employeeService;

        public TeamController(ITeamService teamService,IEmployeeService employeeService)
        {
            this.teamService = teamService;
            this.employeeService = employeeService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Team> teams = await teamService.GetAllAsync();
            return View(teams);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Team team)
        {
            var id= await teamService.AddEntity(team);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var _data = await teamService.GetByIdAsync(id);
            return View(_data);
        }
 
        [HttpPost]
        public async Task<IActionResult> Update(Team team)
        {
            bool isSuccess = await teamService.UpdateEntity(team);
            if (isSuccess)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var _data=await teamService.GetByIdAsync(id);
            return View(_data);
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
           bool isSuccess= await teamService.DeleteEntity(id);
            if (isSuccess)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
