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

        [Range(0,int.MaxValue,ErrorMessage ="Please Select Service Type")]
        public int ServiceTypeId { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        [MinLength(5,ErrorMessage ="Customer Name must be at least 5 characters")]
        public string? Name { get; set; } = String.Empty;

        [Required(ErrorMessage ="Address is required")]
        [MinLength(5, ErrorMessage = "Address at least 5 Character")]
        public string? Address { get; set; } = String.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Please Select Sub Area")]
        public int SubAreaId { get; set; } //FK
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; } = 0;
        [Range(0, int.MaxValue, ErrorMessage = "Please Select Contact")]
        public int ContactId { get; set; } //FK
        [Range(0, int.MaxValue, ErrorMessage = "Please Select Marketing Officer")]
        public int EmployeeId { get; set; }//FK
        public string? ModifiedBy { get; set; } = "";

        public List<BuildingDetailsVM>? BuildingDetails { get; set; }
        public List<ContractDetailsVM>? ContractDetails { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CallingDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        [Required]
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        [Range(0, int.MaxValue, ErrorMessage = "Please Select Working Month")]
        public int CustomerDoTheWorkingMonth { get; set; }

        public string? Remarks { get; set; } = String.Empty;

        [Required]
        [Display(Name = "Positive/Negative")]
        public string? PositiveOrNegative { get; set; } = String.Empty;

        [Required(ErrorMessage = "Discussion is required")]
        public string? DiscussionDetailsNote { get; set; } = String.Empty;

        public string? MarketingNextPlan { get; set; } = "";

        [Required]
        [DataType(DataType.Date)]
        public DateTime FollowupCallDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        [Required]
        public string? Status { get; set; } = String.Empty;//Pending Or Confirm

        public int CustomerId { get; set; } = 0;//FK

        [Range(0, int.MaxValue, ErrorMessage = "Please Select Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please select Reference By")]
        public string ReferenceBy { get; set; }

        public IEnumerable<Designation>? DesignationList { get; set; }
        public IEnumerable<City>? CityList { get; set; }
        public IEnumerable<ContactBy>? ContactList { get; set; }
        public IEnumerable<Employee>? EmployeeList { get; set; }
        public IEnumerable<MonthList>? MonthList { get; set; }
        public IEnumerable<ServiceType>? ServiceList { get; set; }
        public IEnumerable<Brand>? BrandList { get; set; }
        public IEnumerable<Category>? CategoryList { get; set; }

    }
}
