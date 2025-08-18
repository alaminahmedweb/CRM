using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ChartOfAccount:BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string AccountType { get; set; }
    }
}
