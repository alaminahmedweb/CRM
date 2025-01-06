namespace Web.ViewModels
{
    public class UpdateBookingAmountVM
    {
        public int FollowupId { get; set; }
        public double AgreeAmount { get; set; } = 0;
        public string ModifiedBy { get; set; } = "";
        public string Remarks { get; set; } = "";

    }
}
