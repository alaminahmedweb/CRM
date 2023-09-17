using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDashboardQueryService _dashboardQueryService;

        public HomeController(ILogger<HomeController> logger,IDashboardQueryService dashboardQueryService)
        {
            _logger = logger;
            this._dashboardQueryService = dashboardQueryService;
        }
        public IActionResult Index()
        {
            var data= _dashboardQueryService.GetDashboardData();
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}