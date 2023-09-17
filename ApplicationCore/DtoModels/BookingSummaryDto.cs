using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class BookingSummaryDto
    {
        public int? FollowupId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; } = DateTime.Now;
        public double BookingAmtPerSlot { get; set; } = 0;
        public int NoOfSlot { get; set; } = 0;
        public double TotalBookingAmt { get; set; } = 0;
        public string ServiceName { get; set; } = "";


    }
}
