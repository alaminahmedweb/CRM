using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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

        //[HttpGet("ViewAttachment/{id}")]
        public async Task<IActionResult> ViewAttachment(int id)
        {
            var transaction = _transactionService.FindData(id => id == id);

            //if (transaction == null || transaction.Attachment == null)
            //    return NotFound("Attachment not found");

            // Return the image/file
            foreach(var  item in transaction)
            {
                return File(item.Attachment, item.AttachmentContentType,
                            item.AttachmentFileName);

            }
            return null;
        }

        public async Task<IActionResult> PreviewAttachment(int id)
        {
            var transaction = await _transactionService.GetDataByIdAsync(id);

            if (transaction == null || transaction.Attachment == null)
                return NotFound("Attachment not found");

            // For images: display in browser
            if (transaction.AttachmentContentType.StartsWith("image/"))
            {
                return File(transaction.Attachment, transaction.AttachmentContentType);
            }
            // For PDFs: display in browser
            else if (transaction.AttachmentContentType == "application/pdf")
            {
                return File(transaction.Attachment, transaction.AttachmentContentType);
            }
            // For other files: force download
            else
            {
                return File(transaction.Attachment, transaction.AttachmentContentType,
                            transaction.AttachmentFileName);
            }
        }
    }
}
