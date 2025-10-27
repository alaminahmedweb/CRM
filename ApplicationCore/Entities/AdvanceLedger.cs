using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class AdvanceLedger : BaseEntity
    {
        public int TrNo { get; set; } = 0;
        public DateTime TrDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public DateTime AdvanceGivenDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public string NameOfReceipent {  get; set; }
        public string Designation { get; set; }
        public string Purpose {  get; set; }
        public double AdvanceAmt { get; set; } = 0;
        public double AdjustAmt { get; set; } = 0;
        public bool Valid { get; set; } = true;
        public int IsApproved { get; set; } = 0;
        public string ApprovedBy { get; set; } = "";
        public DateTime ApprovedDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

    }
}
