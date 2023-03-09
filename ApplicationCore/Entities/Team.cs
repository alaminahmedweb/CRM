using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Team : BaseEntity
    {
        [Required]
        [MinLength(3, ErrorMessage = "Mpo Name at least 3 Character")]
        public string Name { get; set; }=String.Empty;

        public string TeamLeaderName { get; set; }

        [Required]
        public string Status { get; set; } = String.Empty;

    }
}
