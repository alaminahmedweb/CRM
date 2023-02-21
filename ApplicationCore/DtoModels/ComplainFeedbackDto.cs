using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class ComplainFeedbackDto
    {
        public int Id { get; set; }
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
        public DateTime ComplainDate { get; set; }
        public string ComplainDetails { get; set; }
        public DateTime? ActionTakenDate { get; set; } = DateTime.Now;
        public string? ActionTakenAgainstComplain { get; set; }

    }
}
