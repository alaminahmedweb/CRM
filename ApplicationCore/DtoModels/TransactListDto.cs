using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class TransactListDto
    {
        public int TrNo { get; set; }

        public DateTime TrDate { get; set; } = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
        public string VoucherType { get; set; }
        public string VoucherNo { get; set; }
        public string Remarks { get; set; } = "";
    }
}
