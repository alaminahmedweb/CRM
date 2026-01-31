using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class tmpCustomerList : BaseEntity
    {
        public int CustomerId { get; set; }
        public string CustomerName { get;set; }
        public string Address { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string ContractPerson { get; set; }
        public string MobileNo { get; set; }
        public string Designation { get; set; }
        public string EmployeeName { get; set; }
        public string CityName { get; set; }

    }
}
