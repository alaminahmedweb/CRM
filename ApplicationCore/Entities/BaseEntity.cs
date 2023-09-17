using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationCore.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; } = 0;

        [DataType(DataType.DateTime)]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        public string ModifiedBy { get; set; } ="" ; 
    }
}
