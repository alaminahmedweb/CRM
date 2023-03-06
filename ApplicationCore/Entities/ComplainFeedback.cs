using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class ComplainFeedback
    {
        public int Id { get; set; }
        [Required]
        public string ActionTakenAgainstComplain { get; set; } = "";
        [Required]
        public DateTime ActionTakenDate { get; set; }

        [ForeignKey("ComplainRegister")]
        public int ComplainId { get; set; }
        public ComplainRegister ComplainRegister { get; set; }

    }
}
