using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Transact : BaseEntity
    {
        public int TrNo {  get; set; }
        public DateTime TrDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public string VoucherType {  get; set; }
        public string VoucherNo { get; set; }
        public string Code { get; set; } = "";
        public string Description { get; set; } = "";
        public string Narration { get; set; } = "";
        public double Debit { get; set; }
        public double Credit {  get; set; }
        public string Remarks { get; set; } = "";
        public int Valid { get; set; } = 5;
        public string ApprovedBy { get; set; } = "";
        public DateTime ApprovedDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        // Add these properties for image support
        public byte[]? Attachment { get; set; }
        public string? AttachmentFileName { get; set; }
        public string? AttachmentContentType { get; set; }
        public long? AttachmentSize { get; set; }

        [NotMapped]  // This won't be stored in the database
        public bool HasAttachment { get; set; }
    }
}
