using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.ViewModels
{
    public class ComplainFeedbackVM
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; }
        public string TeamName { get; set; }
        public string TeamLeaderName { get; set; }
        public string ShiftName { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public double Amount { get; set; }
        public int ComplainId { get; set; }
        [DataType(DataType.Date)]
        public DateTime ComplainDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        [Required]
        public string ComplainDetails { get; set; }
        [Required]
        public DateTime ActionTakenDate { get; set; }= TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        [Required]
        public string ActionTakenAgainstComplain { get; set; }

    }
}
