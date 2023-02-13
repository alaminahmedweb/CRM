using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class ServiceFeedbackDto
    {
        public int Id { get; set; }//Feedback Id
        public int BookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string TeamName { get; set; }
        public string ShiftName { get; set; }
        public string CustomerName { get; set; }
        public string ContractPerson { get; set; }
        public string Address { get; set; }
        public string AreaName { get; set; }
        public string MobileNo { get; set; }
        public double Amount { get; set; }
        public string BuildingDetails { get; set; }
        public string FeedbackDetails { get; set; }
        public DateTime? FeedbackEntryDate { get; set; }

    }
}
