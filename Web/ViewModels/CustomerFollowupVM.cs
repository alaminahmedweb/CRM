using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CustomerFollowupVM
    {
        public CustomerFollowupVM()
        {
            BuildingDetails = new List<BuildingDetailsVM>();
            ContractDetails= new List<ContractDetailsVM>();
        }
        public int Id { get; set; } = 0;
        public int serviceTypeId { get; set; }
        public string? Name { get; set; } = String.Empty;
        public string? ContractPerson { get; set; } = String.Empty;

        public string? DesignationId { get; set; } = String.Empty;
        public string? MobileNo { get; set; } = String.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Address at least 5 Character")]
        public string? Address { get; set; } = String.Empty;

        [Required]
        public int CityId { get; set; }

        [Required]
        public int AreaId { get; set; } = 0;//FK

        [Required]
        public int SubAreaId { get; set; } = 0;//FK
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; }
        public int BrandId { get; set; } = 0;
        public int Quantity { get; set; }
        public int Capacity { get; set; }
        public int ContactId { get; set; } = 0;//FK
        public int MpoId { get; set; } = 0;//FK

        public List<BuildingDetailsVM> BuildingDetails { get; set; }
        public List<ContractDetailsVM> ContractDetails { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? CallingDate { get; set; } = DateTime.Now;
        [Required]
        public string? OpinionAndPlan { get; set; } = String.Empty;
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
