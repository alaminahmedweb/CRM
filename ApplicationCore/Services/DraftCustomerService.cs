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
    public class DraftCustomerService : IDraftCustomerService
    {
        private readonly IRepository<DraftCustomer> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public DraftCustomerService(IRepository<DraftCustomer> repository, IUnitOfWok unitOfWok)
        {
            this._repository = repository;
            this._unitOfWok = unitOfWok;
        }
        public async Task<int> AddEntity(DraftCustomer entity)
        {
            await _repository.AddEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            await _repository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }

        public IEnumerable<DraftCustomer> Find(Expression<Func<DraftCustomer, bool>> expression)
        {
            return _repository.Find(expression);
        }

        public async Task<IEnumerable<DraftCustomer>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DraftCustomer> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> IsRecordExistsAsync(Expression<Func<DraftCustomer, bool>> expression)
        {
            return await _repository.IsRecordExistsAsync(expression);
        }

        public async Task<bool> UpdateEntity(DraftCustomer entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;

        }
    }
}
