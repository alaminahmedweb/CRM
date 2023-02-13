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
        public string? ContractPerson { get; set; } = String.Empty;
        public string? MobileNo { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string? BuildingDetails { get; set; } = String.Empty;
        public string AreaName { get; set; } = "";
        public double? AgreeAmount { get; set; } = 0;
        public DateTime? EntryDate { get; set; }=DateTime.Now;
        public DateTime? BookingDate { get; set; } = DateTime.Now;

        public int TeamId { get; set; }

        public int ShiftId { get; set; }

        public int FollowupId { get; set; }

        public string Status { get; set; } = String.Empty;//Pending Or Confirm
        public List<BookingItemDto> BookingItems { get; set; }


    }
}
