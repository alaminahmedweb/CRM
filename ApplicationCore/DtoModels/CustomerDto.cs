using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public int FollowupId { get; set; }
        public string ClientName { get; set; } = String.Empty;
        public string CustomerName { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public int EmployeeId { get; set; }//FK
        public string EmployeeName { get; set; }
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; } = 0;
        public int ContactId { get; set; } //FK
        public string ContactName { get; set; } = "";//FK
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int SubAreaId { get; set; }
        public string SubAreaName { get; set; }
        public string MobileNo { get; set; }

        //[DataType(DataType.Date)]
        //public DateTime FollowupDate { get; set; }
        public string FollowupDate { get; set; }
        public List<BuildingDetailsDto> BuildingDetails { get; set; } = new List<BuildingDetailsDto>();
        public List<ContractDetailsDto> ContractDetails { get; set; }=new List<ContractDetailsDto>();
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ReferenceBy { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public string ServiceName { get; set; }
        public string Status { get; set; }
    }
}
