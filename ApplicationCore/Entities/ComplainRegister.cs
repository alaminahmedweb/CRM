using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ComplainRegister : BaseEntity
    {
        [Required]
        [MinLength(4)]
        public string ComplainDetails { get; set; }
        [Required]
        public DateTime ComplainDate { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public bool IsGivenFeedback { get; set; } = false;

    }
}
