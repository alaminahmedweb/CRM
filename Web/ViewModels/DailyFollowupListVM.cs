using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class DailyFollowupListVM
    {
        public int CustomerId { get; set; }
        public int FollowupId { get; set; } = 0;
        public string CustomerName { get; set; } = String.Empty;
        public string ContractPerson { get; set; } = String.Empty;
        public string MobileNo { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string BuildingDetails { get; set; } = String.Empty;
        public string CityName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string EmployeeName { get; set; }

        [DataType(DataType.Date)]
        public DateTime FollowupDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

    }
}
