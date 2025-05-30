﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Booking : BaseEntity
    {
        [Required]
        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team? Team { get; set; }

        [Required]
        [ForeignKey("Shift")]
        public int ShiftId { get; set; }
        public Shift? Shift { get; set; }

        [Required]
        [ForeignKey("Followup")]
        public int FollowupId { get; set; }
        public Followup? Followup { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }=DateTime.Now;

        [Required]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        public string Status { get; set; } = "Booked";//Booked/Cancelled

        [ForeignKey("Employee")]
        public int BookingById { get; set; }
        public Employee? Employee { get; set; }
        public string? BookingNote { get; set; } = "";
        public DateTime PaymentDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public string PaymentStatus { get; set; } = "Paid";//Paid/Due

        public DateTime PendingShiftDate { get; set; }
        public DateTime PendingEntryDate { get; set; }
        public int PendingTeamId { get; set; }
        public int PendingShiftId { get; set; }
        public int PendingBookingById { get; set; }
        public string? PendingBookingNote { get; set; } = "";
    }
}
