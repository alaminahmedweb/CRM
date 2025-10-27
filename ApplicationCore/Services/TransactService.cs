using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class TransactService : ITransactService
    {
        private readonly IRepository<Transact> _transactionRepository;
        private readonly IUnitOfWok _unitOfWok;
        private readonly ITransactQueryService _transactQueryService;
        public TransactService(IRepository<Transact> transactionRepository,
            IUnitOfWok unitOfWok,ITransactQueryService transactQueryService)
        {
            this._transactionRepository = transactionRepository;
            this._unitOfWok = unitOfWok;
            this._transactQueryService = transactQueryService;
        }

        public async Task<int> AddEntity(TransactDto entity)
        {
            int transactionId = 0;
            _unitOfWok.BeginTransaction();
            try
            {
                int trno= _transactQueryService.GetMaxTrNo();
                foreach (var item in entity.TransactionDetails)
                {
                    Transact transaction = new Transact();
                    transaction.VoucherNo = item.VoucherNo;
                    transaction.VoucherType = item.VoucherType;
                    transaction.Code = item.Code;
                    transaction.Description = item.Description;
                    transaction.Narration = item.Narration;
                    transaction.Debit = item.Debit;
                    transaction.Credit = item.Credit;
                    transaction.Valid = 2;
                    transaction.ModifiedBy = item.ModifiedBy;
                    transaction.Remarks = item.Remarks;
                    transaction.TrNo = trno;
                    transactionId= await _transactionRepository.AddEntity(transaction);
                }
                
                await _unitOfWok.SaveChangesAsync();
                _unitOfWok.CommitTransaction();
                return transactionId;
            }
            catch (Exception ex)
            {
                _unitOfWok.RollbackTransaction();
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TransactDto> Find(Expression<Func<TransactDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TransactDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TransactDto> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<TransactDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEntity(TransactDto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateMultipleEntity(List<int> entity, string userName,int valid)
        {
            bool isSuccessUpdate = false;
            _unitOfWok.BeginTransaction();
            try
            {
                foreach (var item in entity)
                {
                    var data = await _transactionRepository.GetByIdAsync(item);
                    data.Valid = valid;
                    data.ApprovedBy = userName;
                    if (valid == 1)
                    {
                        data.TrDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
                    }
                    data.ApprovedDate= TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
                    isSuccessUpdate = await _transactionRepository.UpdateEntity(data);
                }

                await _unitOfWok.SaveChangesAsync();
                _unitOfWok.CommitTransaction();
                return isSuccessUpdate;
            }
            catch (Exception ex)
            {
                _unitOfWok.RollbackTransaction();
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
