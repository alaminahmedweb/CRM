using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class tmpDailyReceiveAndPayment : BaseEntity
    {
        public int SlNo { get; set; }
        public int SLNo2 { get; set; }
        public string Code { get; set; }
        public string Code2 { get; set; }
        public string Particulars { get; set; }
        public string Particulars2 { get; set; }
        public double Cash { get; set; }
        public double Cash2 { get; set; }
        public double Bank { get; set; }
        public double Bank2 { get; set; }
        public double Total { get; set; }
        public double Total2 { get; set; }
    }
}
