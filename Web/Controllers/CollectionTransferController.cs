using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class CollectionTransferController : Controller
    {
        private readonly ITransactQueryService _transactQueryService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CollectionTransferController(ITransactQueryService transactQueryService, 
            UserManager<ApplicationUser> userManager)
        {
            _transactQueryService = transactQueryService;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetCollectionAmtByDate(DateTime collectionDate)
        {
            var data = _transactQueryService.GetCollectionAmtByDate(collectionDate);
            return Json(data);
        }

        public async Task<JsonResult> TransferCollectionByDate(DateTime collectionDate)
        {
            string userName = _userManager.GetUserId(User);

            var data =await _transactQueryService.TransferTransact(collectionDate,userName);
            return Json(data);
        }


    }
}
