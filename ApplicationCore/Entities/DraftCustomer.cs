using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public DateTime NextFollowupDate { get; set; } = DateTime.Now;
        public bool IsFollowupDone { get; set; } = false;

    }
}
