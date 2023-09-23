using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class ComplainFeedbackDto
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; }

        public string TeamName { get; set; }
        public string TeamLeaderName { get; set; }
        public string ShiftName { get; set; }
        public string? MobileNo { get; set; } = String.Empty;
        public string? BookingNote { get; set; } = String.Empty;
        public string? ServiceName { get; set; } = String.Empty;
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public double Amount { get; set; }
        public int ComplainId { get; set; }

        [DataType(DataType.Date)]
        public DateTime ComplainDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        public string ComplainDetails { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Action Date")]
        public DateTime? ActionTakenDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        [Required]
        [Display(Name = "Action Details")]
        public string? ActionTakenAgainstComplain { get; set; }

        public string ModifiedBy { get; set; } = "";
    }
}
