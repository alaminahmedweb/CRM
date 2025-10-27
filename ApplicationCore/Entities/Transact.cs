using System;
using System.Collections.Generic;
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
        public string Code { get; set; }
        public string Description { get; set; }
        public string Narration { get; set; }
        public double Debit { get; set; }
        public double Credit {  get; set; }
        public string Remarks { get; set; }
        public int Valid { get; set; } = 5;
        public string ApprovedBy { get; set; } = "";
        public DateTime ApprovedDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

    }
}
