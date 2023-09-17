using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ContractDetails : BaseEntity
    {
        [Required]
        [Display(Name ="Contract Person")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Mobile No")]
        public string MobileNo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please Select Designation")]
        [ForeignKey("Designation")]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        public Designation? Designation { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
