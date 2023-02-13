using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class FollowupDto
    {

        [DataType(DataType.Date)]
        public DateTime CallingDate { get; set; } = DateTime.Now;
        public string OpinionAndPlan { get; set; } = String.Empty;
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public string CustomerDoTheWorkingMonth { get; set; } = String.Empty;
        public string Remarks { get; set; } = String.Empty;
        public string PositiveOrNegative { get; set; } = String.Empty;
        public string DiscussionDetailsNote { get; set; } = String.Empty;
        public string MarketingNextPlan { get; set; } = String.Empty;
        [DataType(DataType.Date)]
        public DateTime FollowupCallDate { get; set; }=DateTime.Now;
        public string Status { get; set; } = String.Empty;//Pending Or Confirm

    }
}
