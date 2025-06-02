using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class BookingItemDto
    {
        [DataType(DataType.Date)]
        public DateTime? EntryDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        public int? ShiftId { get; set; }
        public string ShiftName { get; set; }
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? TeamLeaderName { get; set; }
        public string? WorkingMonth { get; set; }
        public int? FollowupId { get; set; }
        public int? BookingId { get; set; }
        public string EmployeeName { get; set; } = "";//FK
        public string ServiceName { get; set; } = "";
        public int CustomerId { get; set; }
        public string? Name { get; set; } = String.Empty;
        public string? ContractPerson { get; set; } = String.Empty;
        public string? MobileNo { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string CityName { get; set; } = "";
        public string? AreaName { get; set; } = "";//FK
        public string SubAreaName { get; set; } = "";
        public string? BuildingDetails { get; set; } = String.Empty;
        public string? Remarks { get; set; } = String.Empty;
        public double AgreeAmount { get; set; } = 0;
        public string? Status { get; set; } = "";

        [DataType(DataType.Date)]
        public DateTime? FollowupCallDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        [DataType(DataType.Date)]
        public DateTime? TrDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        [Required]
        public string? CustomerFeedback { get; set; }
        [Required]
        public string? CompanyFeedback { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FeedbackEntryDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        [DataType(DataType.Date)]
        public DateTime ComplainDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public string ComplainDetails { get; set; }
        public string? BookingNote { get; set; } = "";
        public DateTime ModifiedDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        public string BookingEntryDate { get; set; } 
        public string BookingWorkingDate { get; set; }
        public double PendingAgreeAmount { get; set; } = 0;


    }
}
