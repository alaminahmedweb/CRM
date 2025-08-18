using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class AccountsReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
