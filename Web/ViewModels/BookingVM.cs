using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class BookingVM
    {
        public BookingVM()
        {
            BookingItems = new List<BookingItemVM>();
        }

        public string? Name { get; set; } = String.Empty;
        public string? ContractPerson { get; set; } = String.Empty;
        public string? MobileNo { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;
        [Required]
        public string? BuildingDetails { get; set; } = String.Empty;
        public string AreaName { get; set; } = "";
        public double AgreeAmount { get; set; } = 0;
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }=DateTime.Now;

        [Required]
        public int TeamId { get; set; }
        
        [Required]
        public int ShiftId { get; set; }

        [Required]
        public int FollowupId { get; set; }

        [Required]
        public string Status { get; set; } = String.Empty;//Pending Or Confirm
        public List<BookingItemVM> BookingItems { get; set; }

    }
}
