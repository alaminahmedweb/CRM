using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class CustomerFollowupDto
    {
        //Customer Portion
        public string? Name { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string AreaName { get; set; } = "";//FK
        public string EmployeeName { get; set; } = "";//FK
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; }
        public string CityName { get; set; }
        public int SubAreaId { get; set; }
        public string SubAreaName { get; set; }
        public int ContactId { get; set; }
        public string? ContactName { get; set; } = String.Empty;
        public int EmployeeId { get; set; }
        public List<BuildingDetailsDto> BuildingDetails { get; set; }= new List<BuildingDetailsDto>();
        public List<ContractDetailsDto> ContractDetails { get; set; }=new List<ContractDetailsDto>();


        //Followup Portion
        [DataType(DataType.Date)]
        public DateTime CallingDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public int CustomerDoTheWorkingMonth { get; set; } = 0;
        public string CustomerDoTheWorkingMonthName { get; set; } = "";
        public string? Remarks { get; set; } = String.Empty;
        public string? PositiveOrNegative { get; set; } = String.Empty;
        public string? DiscussionDetailsNote { get; set; } = String.Empty;
        public string? MarketingNextPlan { get; set; } = "";
        [DataType(DataType.Date)]
        public DateTime FollowupCallDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public string Status { get; set; } = String.Empty;//Pending Or Confirm
        public int ServiceTypeId { get; set; }
        public string ServiceName { get; set; } = "";
        public int CustomerId { get; set; } = 0;//FK
        public string ModifiedBy { get; set; } = "";
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ReferenceBy { get; set; }
        public int FollowupId { get; set; }
        public int FollowupById { get; set; }
        public int BookingById { get; set; }
    }
}
