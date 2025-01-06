using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class DraftCustomerVM
    {
 
        [DataType(DataType.DateTime)]
        public DateTime ModifiedDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        public string ModifiedBy { get; set; } = "";

        public string ClientName { get; set; } = String.Empty;
        public string MobileNo { get; set; } = String.Empty;
        public string Remarks { get; set; } = String.Empty;
        [DataType(DataType.Date)]
        public DateTime NextFollowupDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public bool IsFollowupDone { get; set; } = false;
        [Required]
        [Range(1, int.MaxValue,ErrorMessage ="Please Select Contact")]
        public int ContactId { get; set; }//FK
        public IEnumerable<ContactBy>? ContactList { get; set; }


    }
}
