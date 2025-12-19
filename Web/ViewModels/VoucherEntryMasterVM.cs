using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class VoucherEntryMasterVM
    {
        [DataType(DataType.Date)]
        public DateTime VoucherDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        [Required(ErrorMessage = "Voucher No is required")]
        public string? VoucherNo { get; set; } = String.Empty;

        public string? VoucherType { get; set; } = String.Empty;
        public string? ModifiedBy { get; set; } = "";
        [Required(ErrorMessage = "Remarks is required")]
        public string Remarks { get; set; } = "";

        public IFormFile? Attachment { get; set; }
        public string? AttachmentDescription { get; set; }
    }
}
