using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class FollowupDetailsByIdDto
    {
        public FollowupDetailsByIdDto()
        {
            Followups = new List<FollowupDto>();
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string CityName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string EmployeeName { get; set; }
        public string ContactName { get; set; }
        public string CategoryName { get; set; }
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; } = 0;
        public string ReferenceBy { get; set; }

        public string Status { get; set; } =String.Empty;
        public int FollowupId { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public List<FollowupDto> Followups { get; set; }
        public List<BookingItemDto> Bookings { get; set; } = new List<BookingItemDto>();
        public List<BookingSummaryDto> BookingSummary { get; set; } = new List<BookingSummaryDto>();
        public List<BuildingDetailsDto> BuildingDetails { get; set; } = new List<BuildingDetailsDto>();
        public List<ContractDetailsDto> ContractDetails { get; set; } = new List<ContractDetailsDto>();
        public List<ComplainFeedbackDto> Complains { get; set; } = new List<ComplainFeedbackDto>();

    }
}
