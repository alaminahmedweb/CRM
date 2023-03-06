using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class CustomerFollowupMasterDto
    {
        public CustomerFollowupDto CustomerFollowup { get; set; }
        public List<BuildingDetailsDto> BuildingDetails { get; set; }
        public List<ContractDetailsDto> ContractDetails { get; set; }

    }
}
