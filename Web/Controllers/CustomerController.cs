using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
