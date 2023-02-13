using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(3, ErrorMessage = "Mpo Name at least 3 Character")]
        public string Name { get; set; }=String.Empty;
        public bool isMpo { get; set; }
        [Required]
        public string Status { get; set; }= String.Empty;
    }
}
