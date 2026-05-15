using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class NextFiveDaysVacantDto
    {
        public DateTime BookingDate { get; set; }
        public string ShiftName { get; set; }
        public int TotalVacant { get; set; }
    }
}
