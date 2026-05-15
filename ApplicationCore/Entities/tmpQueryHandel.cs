using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class tmpQueryHandel : BaseEntity
    {
        public string Code { get; set; }
        public double TrNo { get; set; }
        public double Row_Id { get; set; }
        public string Instrument { get; set; }
        public string Remark { get; set; }
        public DateTime Trans_dt { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string ContraDesc { get; set; }
        public string VoucherType { get; set; }
        public string ACHead { get; set; }
        public double opbal { get; set; }
        public string Remarks { get; set; }
        public string ContraHead { get; set; }


    }
}
