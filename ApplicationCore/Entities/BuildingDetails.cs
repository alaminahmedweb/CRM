using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class BuildingDetails : BaseEntity
    {
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please Select Brand")]
        [Display(Name = "Brand Name")]
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand? Brand { get; set; }

        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public string Capacity { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
