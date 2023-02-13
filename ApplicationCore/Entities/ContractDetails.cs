using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class ContractDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string MobileNo { get; set; }

        [ForeignKey("Designation")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
