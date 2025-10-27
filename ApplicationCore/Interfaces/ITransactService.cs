using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ITransactService: IRepository<TransactDto>
    {
        Task<bool> UpdateMultipleEntity(List<int> entity,string userName,int valid);

    }
}
