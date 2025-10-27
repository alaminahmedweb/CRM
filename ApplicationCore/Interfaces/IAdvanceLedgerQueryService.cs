using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdvanceLedgerQueryService
    {
        int GetMaxTrNo();
        List<AdvanceLedger> GetAllPendingAdvanceEntry();
        List<AdvanceLedger> GetAllUnjustedAdvanceEntry();
        AdvanceLedger GetUnjustedAdvanceEntryByTrNo(int trNo);
    }
}
