using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class SubArea : BaseEntity
    {
        public string Name { get; set; } = "";

        [ForeignKey("Area")]
        public int AreaId { get; set; }
        public Area? Area { get; set; }
    }
}
