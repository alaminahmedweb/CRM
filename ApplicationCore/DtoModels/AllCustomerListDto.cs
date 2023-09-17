using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class AllCustomerListDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string EmployeeName { get; set; }
        public string ContactName { get; set; } = "";//FK
        public string CityName { get; set; }
        public string AreaName { get; set; }
        public string SubAreaName { get; set; }
        public string CategoryName { get; set; }
        public string ReferenceBy { get; set; }
        public string MobileNo { get; set; }
        public string DesignationName { get; set; }
        public string Status { get; set; }

    }
}
