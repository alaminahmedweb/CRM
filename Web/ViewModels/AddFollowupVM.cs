using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;
using TimeZoneConverter;

namespace Web.ViewModels
{
    public class AddFollowupVM 
    {
        //Customer Portion
        public string? Name { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string AreaName { get; set; } = "";//FK
        public string CityName { get; set; } = "";
        public string SubAreaName { get; set; } = "";
        public string? EmployeeName { get; set; } = String.Empty;
        public List<BuildingDetailsVM>? BuildingDetails { get; set; }
        public List<ContractDetailsVM>? ContractDetails { get; set; }
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; } = 0;
        public string ContactName { get; set; } = "";


        //Followup Portion
        [Required]
        [DataType(DataType.Date)]
        public DateTime? CallingDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        [Required]
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Please Select Working Month")]
        public int CustomerDoTheWorkingMonth { get; set; }
        public string? Remarks { get; set; } = String.Empty;
        [Required]
        [Display(Name = "Positive/Negative")]
        public string? PositiveOrNegative { get; set; } = String.Empty;
        [Required]
        public string? DiscussionDetailsNote { get; set; } = String.Empty;
        public string? MarketingNextPlan { get; set; } = String.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime FollowupCallDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        [Required]
        public string Status { get; set; } = String.Empty;//Pending Or Confirm

        [Range(0, int.MaxValue, ErrorMessage = "Please Select Service Type")]
        public int ServiceTypeId { get; set; } = 0;
        public string ModifiedBy { get; set; } = "";
        public int CustomerId { get; set; } = 0;//FK
        public string? ReferenceBy { get; set; } = "";
        public string? CategoryName { get; set; } = "";
        [Range(0, int.MaxValue, ErrorMessage = "Please Select Followup By")]
        public int FollowupById { get; set; }
        public int FollowupId { get; set; }
        public bool IsFollowupDone { get; set; } = false;
        public IEnumerable<Employee>? EmployeeList { get; set; }
        public IEnumerable<MonthList>? MonthList { get; set; }
        public IEnumerable<ServiceType>? ServiceList { get; set; }

    }
}
