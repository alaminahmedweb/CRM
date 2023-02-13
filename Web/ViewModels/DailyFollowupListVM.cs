namespace Web.ViewModels
{
    public class DailyFollowupListVM
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = String.Empty;
        public string ContractPerson { get; set; } = String.Empty;
        public string MobileNo { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string BuildingDetails { get; set; } = String.Empty;
        public string AreaName { get; set; }
        public string MpoName { get; set; }

    }
}
