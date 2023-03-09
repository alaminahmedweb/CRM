using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class Area :BaseEntity
    {
        [Required]
        [MinLength(3, ErrorMessage = "Area Name at least 3 Character")]
        public string Name { get; set; }=String.Empty;
        
        [Required]
        public string Status { get; set; } = String.Empty;

        [ForeignKey("City")]
        public int CityId { get; set; }
        public City? City { get; set; }
    }
}
