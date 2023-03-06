using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ServiceFeedback
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        public string CustomerFeedback { get; set; }
        [Required]
        public string CompanyFeedback { get; set; }
        [Required]
        public DateTime EntryDateTime { get; set; }

        [ForeignKey("Booking")]
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }
}
