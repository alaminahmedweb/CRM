using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class BuildingDetailsDto
    {
        public int BrandId { get; set; } = 0;
        public string BrandName { get; set; } = "";
        public int Quantity { get; set; } = 0;
        public double Capacity { get; set; } = 0;
        public string ModifiedBy { get; set; } = "";
    }
}
