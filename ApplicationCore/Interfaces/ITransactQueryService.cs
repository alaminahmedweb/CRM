using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ApplicationCore.Interfaces
{
    public interface ITransactQueryService
    {
        int GetMaxTrNo();
        List<Transact> GetPendingTransactionList();
        List<Transact> GetTransactionListByTrNo(int trNo);

    }
}
