using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class ContractDetailsVM
    {
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Contract Person")]
        public string? Name { get; set; } = "";

        [Required]
        [Display(Name = "Mobile No")]
        public string? MobileNo { get; set; } = "";

        [Range(0, int.MaxValue, ErrorMessage = "Please Select Designation")]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; } = 0;
        public string? Designation { get; set; } = "";
        
        public int CustomerId { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime ModifiedDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");

        public string ModifiedBy { get; set; } = "";
        public IEnumerable<Designation>? DesignationList { get; set; }

    }
}
