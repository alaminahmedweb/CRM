using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class VoucherEntryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
