using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class CustomerFollowupDto
    {
        public string? Name { get; set; } = String.Empty;
        public string? ContractPerson { get; set; } = String.Empty;
        public string? MobileNo { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string? BuildingDetails { get; set; } = String.Empty;
        public string AreaName { get; set; } = "";//FK
        public string EmployeeName { get; set; } = "";//FK
        public DateTime? CallingDate { get; set; } = DateTime.Now;
        public string? OpinionAndPlan { get; set; } = String.Empty;
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public string? CustomerDoTheWorkingMonth { get; set; } = String.Empty;
        public string? Remarks { get; set; } = String.Empty;
        public string? PositiveOrNegative { get; set; } = String.Empty;
        public string? DiscussionDetailsNote { get; set; } = String.Empty;
        public string? MarketingNextPlan { get; set; } = String.Empty;
        public DateTime FollowupCallDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = String.Empty;//Pending Or Confirm
        public int CustomerId { get; set; } = 0;//FK

    }
}
