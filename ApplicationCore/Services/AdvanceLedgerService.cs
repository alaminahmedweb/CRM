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
    public class AdvanceLedgerService : IAdvanceLedgerService
    {
        private readonly IRepository<AdvanceLedger> _repository;
        private readonly IUnitOfWok _unitOfWok;
        public AdvanceLedgerService(IRepository<AdvanceLedger> advanceLedgerRepository,
            IUnitOfWok unitOfWok, ITransactQueryService transactQueryService)
        {
            this._repository = advanceLedgerRepository;
            this._unitOfWok = unitOfWok;
        }

        public async Task<int> AddEntity(AdvanceLedger entity)
        {
            await _repository.AddEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return entity.Id;
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdvanceLedger> Find(Expression<Func<AdvanceLedger, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AdvanceLedger>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AdvanceLedger> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<AdvanceLedger, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEntity(AdvanceLedger entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
