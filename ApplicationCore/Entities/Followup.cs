using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Followup : BaseEntity
    {

        [Required]
        [DataType(DataType.Date)]
        public DateTime CallingDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        [ForeignKey("ServiceType")]
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }


        [Required]
        public double OfferAmount { get; set; } = 0;

        public double AgreeAmount { get; set; } = 0;

        public int CustomerDoTheWorkingMonth { get; set; } = 0;

        public string Remarks { get; set; } = String.Empty;

        [Required]
        public string PositiveOrNegative { get; set; } = String.Empty;

        [Required]
        public string DiscussionDetailsNote { get; set; } = String.Empty;

        public string MarketingNextPlan { get; set; } = String.Empty;
        

        [Required]
        [DataType(DataType.Date)]
        public DateTime FollowupCallDate { get; set; }

        [Required]
        public string Status { get; set; } = String.Empty;//Pending Or Confirm

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }//FK
        public Customer Customer { get; set; }

        [ForeignKey("Employee")]
        public int FollowupById { get; set; } = 0;
        public Employee? Employee { get; set; }
        public bool IsFollowupDone { get; set; } = false;
        public double PendingAgreeAmount { get; set; } = 0;
    }
}
