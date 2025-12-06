using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Web.Controllers
{
    public class VoucherApproveController : Controller
    {
        private readonly ITransactQueryService _transactionQueryService;
        private readonly ITransactService _transactionService;
        private readonly UserManager<ApplicationUser> _userManager;

        public VoucherApproveController(ITransactQueryService transactQueryService,
            ITransactService transactionService,
            UserManager<ApplicationUser> userManager)
        {
            this._transactionQueryService = transactQueryService;
            this._transactionService = transactionService;
            this._userManager = userManager;
        }
        
        [Authorize(Roles = "Super Admin,Admin")]
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetAllPendingPendingTransactData()
        {
            var data = _transactionQueryService.GetPendingTransactionList();
            return Json(data);
        }

        public async Task<JsonResult> ApproveTransaction(int trNo)
        {
            var data= _transactionQueryService.GetTransactionListByTrNo(trNo);

            List<int> list = new List<int>();
            foreach (var item in data)
            {
                int id = item.Id;
                list.Add(id);
            }

            string userName= _userManager.GetUserId(User);
            bool isSuccess = await _transactionService.UpdateMultipleEntity(list, userName,1);
            return Json("Approved");
        }

        public async Task<JsonResult> DeclineTransaction(int trNo)
        {
            var data = _transactionQueryService.GetTransactionListByTrNo(trNo);

            List<int> list = new List<int>();
            foreach (var item in data)
            {
                int id = item.Id;
                list.Add(id);
            }

            string userName = _userManager.GetUserId(User);
            bool isSuccess = await _transactionService.UpdateMultipleEntity(list, userName,3);
            return Json("Declined");
        }

    }
}
