using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries
{
    public class AdvanceLedgerQueryService : IAdvanceLedgerQueryService
    {
        private readonly AppDbContext _dbContext;
        public AdvanceLedgerQueryService(AppDbContext appDbContext)
        {
            this._dbContext = appDbContext;
        }

        public List<AdvanceLedger> GetAllPendingAdvanceEntry()
        {
            var advanceInfo = (from ad in _dbContext.AdvanceLedger.Where(a=>a.IsApproved == 0)
                               select new
                               {
                                   Id = ad.Id,
                                   TrNo = ad.TrNo,
                                   TrDate = ad.TrDate.Date,
                                   AdvanceGivenDate = ad.AdvanceGivenDate.Date,
                                   NameOfReceipent = ad.NameOfReceipent,
                                   Designation = ad.Designation,
                                   Purpose = ad.Purpose,
                                   AdvanceAmt = ad.AdvanceAmt
                               });


            List<AdvanceLedger> advanceLedgerList = new List<AdvanceLedger>();
            foreach (var item in advanceInfo)
            {
                AdvanceLedger advanceLedger = new AdvanceLedger();
                advanceLedger.Id= item.Id;
                advanceLedger.TrNo= item.TrNo;
                advanceLedger.TrDate= item.TrDate;
                advanceLedger.AdvanceGivenDate = item.AdvanceGivenDate;
                advanceLedger.NameOfReceipent = item.NameOfReceipent;
                advanceLedger.Designation = item.Designation;
                advanceLedger.Purpose = item.Purpose;
                advanceLedger.AdvanceAmt = item.AdvanceAmt;
                advanceLedgerList.Add(advanceLedger);
            }
            return advanceLedgerList;
        }

        public List<AdvanceLedger> GetAllUnjustedAdvanceEntry()
        {
            var result = _dbContext.AdvanceLedger
                .Where(a => a.Valid == true && a.IsApproved == 1)
                .GroupBy(a => new
                {
                    a.TrNo,
                    AdvanceGivenDate = a.AdvanceGivenDate.Date,
                    a.NameOfReceipent,
                    a.Designation,
                    a.Purpose
                })
                .Where(g => g.Sum(a => a.AdvanceAmt - a.AdjustAmt) > 0)
                .Select(g => new
                {
                    trno = g.Key.TrNo,
                    AdvanceGivenDate = g.Key.AdvanceGivenDate,
                    NameOfReceipent = g.Key.NameOfReceipent,
                    Designation = g.Key.Designation,
                    Purpose = g.Key.Purpose,
                    DueAmt = g.Sum(a => a.AdvanceAmt - a.AdjustAmt)
                })
                .OrderBy(x => x.NameOfReceipent)
                .ThenBy(x => x.AdvanceGivenDate)
                .ToList();

            List<AdvanceLedger> advanceLedgerList = new List<AdvanceLedger>();
            foreach (var item in result)
            {
                AdvanceLedger advanceLedger = new AdvanceLedger();
                advanceLedger.TrNo = item.trno;
                advanceLedger.AdvanceGivenDate = item.AdvanceGivenDate;
                advanceLedger.NameOfReceipent = item.NameOfReceipent;
                advanceLedger.Designation = item.Designation;
                advanceLedger.Purpose = item.Purpose;
                advanceLedger.AdvanceAmt = item.DueAmt;
                advanceLedgerList.Add(advanceLedger);
            }
            return advanceLedgerList;

        }

        public AdvanceLedger GetUnjustedAdvanceEntryByTrNo(int trNo)
        {
            var result = _dbContext.AdvanceLedger
                .Where(a => a.Valid == true && a.IsApproved == 1)
                .Where(a=>a.TrNo==trNo)
                .GroupBy(a => new
                {
                    a.TrNo,
                    AdvanceGivenDate = a.AdvanceGivenDate.Date,
                    a.NameOfReceipent,
                    a.Designation,
                    a.Purpose
                })
                .Where(g => g.Sum(a => a.AdvanceAmt - a.AdjustAmt) > 0)
                .Select(g => new
                {
                    trno = g.Key.TrNo,
                    AdvanceGivenDate = g.Key.AdvanceGivenDate,
                    NameOfReceipent = g.Key.NameOfReceipent,
                    Designation = g.Key.Designation,
                    Purpose = g.Key.Purpose,
                    DueAmt = g.Sum(a => a.AdvanceAmt - a.AdjustAmt)
                })
                .OrderBy(x => x.trno)
                .ThenBy(x => x.NameOfReceipent)
                .ToList();

            AdvanceLedger advanceLedger = new AdvanceLedger();
            foreach (var item in result)
            {
                advanceLedger.TrNo = item.trno;
                advanceLedger.AdvanceGivenDate = item.AdvanceGivenDate.Date;
                advanceLedger.NameOfReceipent = item.NameOfReceipent;
                advanceLedger.Designation = item.Designation;
                advanceLedger.Purpose = item.Purpose;
                advanceLedger.AdvanceAmt = item.DueAmt;
            }
            return advanceLedger;
        }

        public int GetMaxTrNo()
        {
            var maxTrNo = _dbContext.AdvanceLedger.Any()
                        ? _dbContext.AdvanceLedger.Max(t => t.TrNo)
    : 0;
            var result = maxTrNo + 1;
            return result;
        }
    }
}
