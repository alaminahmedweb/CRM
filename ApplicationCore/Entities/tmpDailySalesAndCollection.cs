using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class tmpDailySalesAndCollection : BaseEntity
    {
        public DateTime TrDate { get; set; }
        public double Sales { get; set; }
        public double Collection { get; set; }
        public double DueCollection { get; set; }
        public double Due { get; set; }
        public double NetCollection { get; set; }
    }
}
