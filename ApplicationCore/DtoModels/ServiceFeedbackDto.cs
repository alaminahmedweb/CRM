using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class ServiceFeedbackDto
    {
        public int Id { get; set; }//Feedback Id
        public int BookingId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; }
        public string TeamName { get; set; }
        [Display(Name ="Team Leader")]
        public string TeamLeaderName { get; set; }
        public string ServiceName { get; set; }
        public string ShiftName { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public double Amount { get; set; }
        public string? BookingNote { get; set; } = "";
        public string? MobileNo { get; set; } = String.Empty;
        [Required]
        public string CustomerFeedback { get; set; }
        [Required]
        public string CompanyFeedback { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EntryDateTime { get; set; }=DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime ComplainDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public string ComplainDetails { get; set; }
        public string ModifiedBy { get; set; } = "";

    }
}
