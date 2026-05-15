using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class VoucherEntryController : Controller
    {

        private readonly IChartOfAccountService _chartOfAccountService;
        private readonly ITransactService _transactionService;

        public VoucherEntryController(IChartOfAccountService chartOfAccountService,
            ITransactService transactionService)
        {
            this._chartOfAccountService = chartOfAccountService;
            this._transactionService = transactionService;
        }

        public IActionResult Index()
        {
            ViewBag.ChartOfAccountList = _chartOfAccountService.Find(a=> ! a.Code.EndsWith("0000"));
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Save(VoucherEntryVM voucherEntryVM)
        {
            if (voucherEntryVM.VoucherEntryDetailsVM == null)
            {
                ModelState.AddModelError("", "Voucher Details Please");
            }

            if (ModelState.IsValid)
            {
                TransactDto transactionDto = new TransactDto();

                foreach (var item in voucherEntryVM.VoucherEntryDetailsVM)
                {
                    TransactDetailsDto transactionDetails = new TransactDetailsDto();
                    transactionDetails.Code = item.AcCode;
                    transactionDetails.Description = item.AcDesc;
                    transactionDetails.Narration = item.Narration;
                    transactionDetails.Debit = item.Debit;
                    transactionDetails.Credit = item.Credit;
                    transactionDetails.VoucherNo = voucherEntryVM.VoucherEntryMasterVM.VoucherNo;
                    transactionDetails.Remarks = voucherEntryVM.VoucherEntryMasterVM.Remarks;
                    transactionDetails.VoucherType = voucherEntryVM.VoucherEntryMasterVM.VoucherType;
                    transactionDetails.ModifiedBy = voucherEntryVM.VoucherEntryMasterVM.ModifiedBy;
                    transactionDetails.TrDate = voucherEntryVM.VoucherEntryMasterVM.VoucherDate;
                    transactionDto.TransactionDetails.Add(transactionDetails);
                }

                if (voucherEntryVM.VoucherEntryMasterVM.VoucherType=="PV" && voucherEntryVM.VoucherEntryMasterVM.Attachment == null)
                {
                    ModelState.AddModelError("Attachment", "Please Upload Image..");
                    return BadRequest(ModelState);
                }

                if (voucherEntryVM.VoucherEntryMasterVM.Attachment != null &&
            voucherEntryVM.VoucherEntryMasterVM.Attachment.Length > 0)
                {
                    // Validate file size (5MB max)
                    if (voucherEntryVM.VoucherEntryMasterVM.Attachment.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Attachment", "File size exceeds 5MB limit.");
                        return BadRequest(ModelState);
                    }

                    // Validate file type
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf", ".doc", ".docx" };
                    var fileExtension = Path.GetExtension(voucherEntryVM.VoucherEntryMasterVM.Attachment.FileName).ToLowerInvariant();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("Attachment", "Invalid file type. Allowed types: JPG, PNG, PDF, DOC.");
                        return BadRequest(ModelState);
                    }

                    using var memoryStream = new MemoryStream();
                    await voucherEntryVM.VoucherEntryMasterVM.Attachment.CopyToAsync(memoryStream);

                    transactionDto.Attachment = memoryStream.ToArray();
                    transactionDto.AttachmentFileName = voucherEntryVM.VoucherEntryMasterVM.Attachment.FileName;
                    transactionDto.AttachmentContentType = voucherEntryVM.VoucherEntryMasterVM.Attachment.ContentType;
                    transactionDto.AttachmentDescription = voucherEntryVM.VoucherEntryMasterVM.AttachmentDescription;
                }

                int transactionId = await _transactionService.AddEntity(transactionDto);
                if (transactionId != 0)
                {
                    TempData["SuccessMessage"] = "Saved Successfully..";
                    return Json(new { redirecturl = "/VoucherEntry/Index/" });
                }
               
            }
            return BadRequest(ModelState);
        }

        //private string GetFileExtension(string contentType)
        //{
        //    return contentType switch
        //    {
        //        "image/jpeg" => ".jpg",
        //        "image/png" => ".png",
        //        "application/pdf" => ".pdf",
        //        "application/msword" => ".doc",
        //        "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => ".docx",
        //        _ => ".dat"
        //    };
        //}

        public async Task<JsonResult> GetList(string param)
        {
            try
            {
                var data = await _chartOfAccountService.GetAllAsync();
                var jsonResult = Json(new { data });
                return jsonResult;
            }
            catch (Exception ex)
            {
                return Json(new { });
            }
        }

    }
}
