using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class AddFollowupDto
    {
        //Followup Portion
        public DateTime? CallingDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public int CustomerDoTheWorkingMonth { get; set; } = 0;
        public string? Remarks { get; set; } = String.Empty;
        public string? PositiveOrNegative { get; set; } = String.Empty;
        public string? DiscussionDetailsNote { get; set; } = String.Empty;
        public string? MarketingNextPlan { get; set; } = String.Empty;
        public DateTime FollowupCallDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public string Status { get; set; } = String.Empty;//Pending Or Confirm
        public int ServiceTypeId { get; set; } = 0;
        public string ModifiedBy { get; set; } = "";
        public int CustomerId { get; set; } = 0;//FK
        public string ReferenceBy { get; set; } = "";
        public string CategoryName { get; set; } = "";
        public int FollowupById { get; set; }
        public int FollowupId { get; set; }
        public bool IsFollowupDone { get; set; }
    }
}
