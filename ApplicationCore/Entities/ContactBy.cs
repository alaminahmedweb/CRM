using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ContactBy
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Contact Name at least 3 Character")]
        public string Name { get; set; }= string.Empty;

        [Required]
        public string Status { get; set; }=string.Empty;
    }
}
