using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name at least 3 Character")]
        public string ClientName { get; set; } = String.Empty;

        [Required]
        [MinLength(5, ErrorMessage = "Address at least 5 Character")]
        public string Address { get; set; } = String.Empty;


        [ForeignKey("SubArea")]
        public int SubAreaId { get; set; }
        public SubArea SubArea { get; set; }

        [Required]
        public int NoOfFloor { get; set; } = 0;

        [Required]
        public int NoOfFlat { get; set; } = 0;

        [ForeignKey("ContactBy")]
        public int ContactId { get; set; }//FK
        public ContactBy ContactBy { get; set; } 


        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }//FK
        public Employee Employee { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string ReferenceBy { get; set; }

    }
}
