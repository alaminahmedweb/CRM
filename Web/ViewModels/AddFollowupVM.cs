using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class AddFollowupVM 
    {
        public int serviceTypeId { get; set; }
        public string? CustomerName { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string AreaName { get; set; } = "";//FK

        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; }
        public int ContactName { get; set; } = 0;//FK
        public int EmployeeName { get; set; } = 0;//FK

        public List<BuildingDetailsVM> BuildingDetails { get; set; }
        public List<ContractDetailsVM> ContractDetails { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? CallingDate { get; set; } = DateTime.Now;
        [Required]
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public int CustomerDoTheWorkingMonth { get; set; } = 0;
        [Required]
        public string? Remarks { get; set; } = String.Empty;
        [Required]
        [Display(Name = "Positive/Negative")]
        public string? PositiveOrNegative { get; set; } = String.Empty;
        [Required]
        public string? DiscussionDetailsNote { get; set; } = String.Empty;
        [Required]
        public string? MarketingNextPlan { get; set; } = String.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime FollowupCallDate { get; set; } = DateTime.Now;
        [Required]
        public string Status { get; set; } = String.Empty;//Pending Or Confirm

        public int CustomerId { get; set; } = 0;//FK



    }
}
