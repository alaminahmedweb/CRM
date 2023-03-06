using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class BookingItemDto
    {
        [DataType(DataType.Date)]
        public string? EntryDate { get; set; }
        [DataType(DataType.Date)]
        public string? BookingDate { get; set; }
        public int? ShiftId { get; set; }
        public string ShiftName { get; set; }
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? TeamLeaderName { get; set; }
        public int? FollowupId { get; set; }
        public string? Name { get; set; } = String.Empty;
        public string? ContractPerson { get; set; } = String.Empty;
        public string? MobileNo { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string CityName { get; set; } = "";
        public string? AreaName { get; set; } = "";//FK
        public string SubAreaName { get; set; } = "";
        public string? BuildingDetails { get; set; } = String.Empty;
        public string? AgreeAmount { get; set; } = "";
        public string? Status { get; set; } = "";

    }
}
