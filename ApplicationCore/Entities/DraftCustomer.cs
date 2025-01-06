using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class DraftCustomer : BaseEntity
    {
        public string ClientName { get; set; } = String.Empty;
        public string MobileNo { get; set; } = String.Empty;
        public string Remarks { get; set; } = String.Empty;
        [DataType(DataType.Date)]
        public DateTime NextFollowupDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public bool IsFollowupDone { get; set; } = false;

        [ForeignKey("ContactBy")]
        public int ContactId { get; set; } = 0;//FK
        public ContactBy? ContactBy { get; set; }

    }
}
