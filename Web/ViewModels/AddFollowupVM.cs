using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class AddFollowupVM : DailyFollowupListVM
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime? CallingDate { get; set; } = DateTime.Now;
        [Required]
        public string? OpinionAndPlan { get; set; } = String.Empty;
        [Required]
        public double? OfferAmount { get; set; } = 0;
        public double? AgreeAmount { get; set; } = 0;
        public string? CustomerDoTheWorkingMonth { get; set; } = String.Empty;
        [Required]
        public string? Remarks { get; set; } = String.Empty;
        [Required]
        public string? PositiveOrNegative { get; set; } = String.Empty;
        [Required]
        public string? DiscussionDetailsNote { get; set; } = String.Empty;
        [Required]
        public string? MarketingNextPlan { get; set; } = String.Empty;
        [Required]
        [DataType(DataType.Date)]
        public DateTime? FollowupCallDate { get; set; } = DateTime.Now;
        [Required]
        public string? Status { get; set; } = String.Empty;//Pending Or Confirm



    }
}
