using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ComplainRegisterVM
    {
        public int Id { get; set; }//Feedback Id
        public int BookingId { get; set; } = 0;

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
        public string ComplainDetails { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ComplainDate { get; set; }=DateTime.Now;

    }
}
