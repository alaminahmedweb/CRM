using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class BookingDto
    {
        public BookingDto()
        {
            BookingItems = new List<BookingItemDto>();
        }
        public int BookingId { get; set; }
        public string? Name { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string AreaName { get; set; } = "";//FK
        public string EmployeeName { get; set; } = "";//FK
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; }
        public string CityName { get; set; }
        public string SubAreaName { get; set; }
        public List<BuildingDetailsDto> BuildingDetails { get; set; } = new List<BuildingDetailsDto>();
        public List<ContractDetailsDto> ContractDetails { get; set; } = new List<ContractDetailsDto>();


        //Followup Portion
        public DateTime? CallingDate { get; set; } = DateTime.Now;
        public double OfferAmount { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public string CustomerDoTheWorkingMonth { get; set; } = "";
        public string? Remarks { get; set; } = String.Empty;
        public string? PositiveOrNegative { get; set; } = String.Empty;
        public string? DiscussionDetailsNote { get; set; } = String.Empty;
        public string? MarketingNextPlan { get; set; } = String.Empty;
        public string ServiceName { get; set; } = "";
        public int CustomerId { get; set; } = 0;//FK

        public DateTime? EntryDate { get; set; }=DateTime.Now;
        public DateTime? BookingDate { get; set; } = DateTime.Now;

        public int TeamId { get; set; }

        public int ShiftId { get; set; }

        public int FollowupId { get; set; }

        public string Status { get; set; } = String.Empty;//Pending Or Confirm
        public List<BookingItemDto> BookingItems { get; set; }


    }
}
