namespace Web.ViewModels
{
    public class BuildingDetailsVM
    {
        public int? Id { get; set; }
        public int BrandId { get; set; } = 0;
        public string? BrandName { get; set; } = "";
        public int Quantity { get; set; } = 0;
        public string Capacity { get; set; } = "";
    }
}
