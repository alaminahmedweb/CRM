using ApplicationCore.DtoModels;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class AddFollowupVM 
    {
        //Customer Portion
        public string? Name { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string AreaName { get; set; } = "";//FK
        public string CityName { get; set; }
        public string SubAreaName { get; set; }
        public string? EmployeeName { get; set; } = String.Empty;
        public List<BuildingDetailsVM> BuildingDetails { get; set; }
        public List<ContractDetailsVM> ContractDetails { get; set; }
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; }
        public int ContactName { get; set; } = 0;//FK


        //Followup Portion
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
        public int ServiceTypeId { get; set; }

        public int CustomerId { get; set; } = 0;//FK



    }
}
