using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class ContractDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string MobileNo { get; set; }

        public int DesignationId { get; set; }
        public string Designation { get; set; }
        public string ModifiedBy { get; set; } = "";
        public int CustomerId { get; set; }

    }
}
