using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CustomerFollowupVM
    {
        public CustomerFollowupVM()
        {
            BuildingDetails = new List<BuildingDetailsVM>();
            ContractDetails = new List<ContractDetailsVM>();
        }
        public int ServiceTypeId { get; set; } = 0;

        [Required(ErrorMessage = "Customer Name is required")]
        [MinLength(5,ErrorMessage ="Customer Name must be at least 5 characters")]
        public string? Name { get; set; } = String.Empty;

        [Required(ErrorMessage ="Address is required")]
        [MinLength(5, ErrorMessage = "Address at least 5 Character")]
        public string? Address { get; set; } = String.Empty;

        [Required]
        public int SubAreaId { get; set; } = 0;//FK
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; } = 0;
        public int ContactId { get; set; } = 0;//FK
        public int EmployeeId { get; set; } = 0;//FK
        public string? ModifiedBy { get; set; } = "";

        public List<BuildingDetailsVM>? BuildingDetails { get; set; }
        public List<ContractDetailsVM>? ContractDetails { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CallingDate { get; set; } = DateTime.Now;

        [Required]
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public int CustomerDoTheWorkingMonth { get; set; } = 0;

        [Required]
        public string? Remarks { get; set; } = String.Empty;

        [Required]
        [Display(Name = "Positive/Negative")]
        public string? PositiveOrNegative { get; set; } = String.Empty;

        [Required(ErrorMessage = "Discussion is required")]
        public string? DiscussionDetailsNote { get; set; } = String.Empty;

        [Required(ErrorMessage = "Next Plan is required")]
        public string? MarketingNextPlan { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime FollowupCallDate { get; set; } = DateTime.Now;

        [Required]
        public string? Status { get; set; } = String.Empty;//Pending Or Confirm

        public int CustomerId { get; set; } = 0;//FK

    }
}
