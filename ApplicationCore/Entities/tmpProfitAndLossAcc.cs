using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class tmpProfitAndLossAcc : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public double InnerAmt { get; set; }
        public double TotalAmt { get; set; }
        public int GroupStatus { get; set; }
        public string GId { get; set; }
        public string GName { get; set; }
    }
}
