using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class FollowupDetailsByIdDto
    {
        public FollowupDetailsByIdDto()
        {
            followups = new List<FollowupDto>();
        }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
        public string AreaName { get; set; }
        public string EmployeeName { get; set; }

        public string Status { get; set; } =String.Empty;
        public int FollowupId { get; set; } = 0;
        public double AgreeAmount { get; set; } = 0;
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; } = 0;
        public List<FollowupDto> followups { get; set; }
        public List<BuildingDetailsDto> BuildingDetails { get; set; } = new List<BuildingDetailsDto>();
        public List<ContractDetailsDto> ContractDetails { get; set; } = new List<ContractDetailsDto>();

    }
}
