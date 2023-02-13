using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Followup
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CallingDate { get; set; } = DateTime.Now;

        [ForeignKey("ServiceType")]
        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }


        [Required]
        public double OfferAmount { get; set; } = 0;

        public double AgreeAmount { get; set; } = 0;

        public int CustomerDoTheWorkingMonth { get; set; } = 0;

        [Required]
        public string Remarks { get; set; } = String.Empty;

        [Required]
        public string PositiveOrNegative { get; set; } = String.Empty;

        [Required]
        public string DiscussionDetailsNote { get; set; } = String.Empty;

        [Required]
        public string MarketingNextPlan { get; set; } = String.Empty;


        [Required]
        [DataType(DataType.Date)]
        public DateTime FollowupCallDate { get; set; }

        [Required]
        public string Status { get; set; } = String.Empty;//Pending Or Confirm

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }//FK
        public Customer Customer { get; set; }
    }
}
