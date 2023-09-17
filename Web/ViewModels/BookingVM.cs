using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class BookingVM
    {
        public BookingVM()
        {
            BookingItems = new List<BookingItemVM>();
        }

        public int Id { get; set; } = 0;

        public string? Name { get; set; } = String.Empty;

        public string? Address { get; set; } = String.Empty;

        public string AreaName { get; set; } = "";//FK

        public string EmployeeName { get; set; } = "";//FK

        public int NoOfFloor { get; set; } = 0;

        public int NoOfFlat { get; set; }

        public string CityName { get; set; }
        public string ContactName { get; set; }
        public string CategoryName { get; set; }
        public string ReferenceBy { get; set; }

        public string SubAreaName { get; set; }

        public List<BuildingDetailsVM> BuildingDetails { get; set; } = new List<BuildingDetailsVM>();

        public List<ContractDetailsVM> ContractDetails { get; set; } = new List<ContractDetailsVM>();
        public string? ModifiedBy { get; set; } = "";


        //Followup Portion
        [DataType(DataType.Date)]
        public DateTime? CallingDate { get; set; } = DateTime.Now;

        public double OfferAmount { get; set; } = 0;

        public double AgreeAmount { get; set; } = 0;

        public string CustomerDoTheWorkingMonth { get; set; } = "";
        public string CustomerDoTheWorkingMonthName { get; set; } = "";

        public string? Remarks { get; set; } = String.Empty;

        public string? PositiveOrNegative { get; set; } = String.Empty;

        public string? DiscussionDetailsNote { get; set; } = String.Empty;

        public string? MarketingNextPlan { get; set; } = String.Empty;

        public string ServiceName { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime FollowupCallDate { get; set; } = DateTime.Now;

        public int CustomerId { get; set; } = 0;//FK

        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }=DateTime.Now;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please Select Team")]
        public int TeamId { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please Select Shift")]
        public int ShiftId { get; set; }

        [Required]
        public int FollowupId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please Select Booking By")]
        public int BookingById { get; set; }

        [Required]
        public string Status { get; set; } = String.Empty;//Pending Or Confirm
        public string? BookingNote { get; set; } = "";

        public List<BookingItemVM> BookingItems { get; set; }
    }
}
