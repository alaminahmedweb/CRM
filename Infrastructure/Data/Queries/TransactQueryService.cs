using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries
{
    public class TransactQueryService : ITransactQueryService
    {
        private readonly AppDbContext _dbContext;
        public TransactQueryService(AppDbContext appDbContext)
        {
            this._dbContext = appDbContext;
        }

        public int GetMaxTrNo()
        {
            var maxTrNo = _dbContext.Transact.Any()
                        ? _dbContext.Transact.Max(t => t.TrNo)
    : 0;
            var result = maxTrNo + 1;
            return result;
        }

        public List<Transact> GetPendingTransactionList()
        {
            var query=_dbContext.Transact.Where(a=>a.Valid==2).ToList();
            List<Transact> list= new List<Transact>();

            foreach(var item in query)
            {
                Transact dto = new Transact();
                dto.TrDate = item.TrDate;
                dto.TrNo=item.TrNo;
                dto.VoucherNo=item.VoucherNo;
                dto.VoucherType=item.VoucherType;
                dto.Code=item.Code;
                dto.Description=item.Description;
                dto.Narration=item.Narration;
                dto.Debit=item.Debit;
                dto.Credit=item.Credit;
                dto.Remarks=item.Remarks;
                list.Add(dto);
            }
            return list;
        }

        public List<Transact> GetTransactionListByTrNo(int trNo)
        {
            var query = _dbContext.Transact.Where(a => a.TrNo == trNo).ToList();
            List<Transact> list = new List<Transact>();

            foreach (var item in query)
            {
                Transact dto = new Transact();
                dto.TrDate = item.TrDate;
                dto.TrNo = item.TrNo;
                dto.VoucherNo = item.VoucherNo;
                dto.VoucherType = item.VoucherType;
                dto.Code = item.Code;
                dto.Description = item.Description;
                dto.Narration = item.Narration;
                dto.Debit = item.Debit;
                dto.Credit = item.Credit;
                dto.Remarks = item.Remarks;
                dto.Id = item.Id;   
                list.Add(dto);
            }
            return list;
        }
    }
}
