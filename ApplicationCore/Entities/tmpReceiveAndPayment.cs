using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class tmpReceiveAndPayment:BaseEntity
    {
        public int SlNo { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public double OpeningBalance { get; set; }
        public double Receive { get; set; }
        public double Payment { get; set; }
        public double ClosingBalance { get; set; }

    }
}
