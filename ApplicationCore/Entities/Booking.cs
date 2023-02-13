using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }

        [Required]
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        public Shift Shift { get; set; }

        [Required]
        [ForeignKey("Followup")]
        public int FollowupId { get; set; }
        public Followup Followup { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? EntryDate { get; set; }=DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Booked";//Booked/Cancelled
    }
}
