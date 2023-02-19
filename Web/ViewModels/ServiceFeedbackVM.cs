﻿using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ServiceFeedbackVM
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
        public string CustomerFeedback { get; set; }
        public string CompanyFeedback { get; set; }
        [DataType(DataType.Date)]
        public DateTime? EntryDateTime { get; set; } = DateTime.Now;

    }
}
