using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class BuildingDetails
    {
        public int Id { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int Quantity { get; set; }
        public double Capacity { get; set; }

        [ForeignKey("Customer")]
        public int CusotmerId { get; set; }
        public Customer Customer { get; set; }
    }
}
