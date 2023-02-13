using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class BookingItemVM
    {
        [DataType(DataType.Date)]
        public DateTime? EntryDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; }
        public int? ShiftId { get; set; }
        public string ShiftName { get; set; }
        public int? TeamId { get; set; }
        public string? TeamName { get; set; }
        public int? FollowupId { get; set; }
        public string? Name { get; set; } = String.Empty;
        public string? ContractPerson { get; set; } = String.Empty;
        public string? MobileNo { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        public string? AreaName { get; set; } = "";//FK
        public string? BuildingDetails { get; set; } = String.Empty;
        public double? AgreeAmount { get; set; } = 0;
        public string? Status { get; set; } = "";

    }
}
