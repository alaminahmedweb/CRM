using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
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
        public CollectionDto GetCollectionAmtByDate(DateTime collectionDate)
        {
            var result = (from bk in _dbContext.Bookings
                          join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                          where bk.PaymentDate.Date == collectionDate.Date &&
                                bk.Status != "Cancel" &&
                                bk.PaymentStatus == "Paid"
                          group fol by bk.PaymentDate into g
                          select new
                          {
                              PaymentDate = g.Key,
                              CollectionAmt = g.Sum(fol => fol.AgreeAmount)
                          }).ToList();

            CollectionDto dto = new CollectionDto();
            foreach (var item in result)
            {
                dto.CollectionAmt = item.CollectionAmt;
                dto.TrDate = item.PaymentDate.Date;
            }
            return dto;
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

        public async Task<ResponseDto> TransferTransact(DateTime trDate,string userName)
        {
            ResponseDto response = new ResponseDto();

            var message =
                    await _dbContext.Database.SqlQuery<string>(
                            @$"exec sp_TransferCollection @TrDate={trDate}, @ModifiedBy={userName}")
                        .ToListAsync();
            foreach(var data in message)
            {
                response.Message = data;
            }
            return response;
        }

    }
}
