using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DtoModels
{
    public class TransactDto
    {
        public List<TransactDetailsDto> TransactionDetails { get; set; } = new List<TransactDetailsDto>();
    }
}
