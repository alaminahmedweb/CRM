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
    public class ContractDetailsService : IContractDetailsService
    {
        private readonly IRepository<ContractDetails> _repository;
        private readonly IUnitOfWok _unitOfWok;
        public ContractDetailsService(IRepository<ContractDetails> repository, IUnitOfWok unitOfWok)
        {
            _repository = repository;
            _unitOfWok = unitOfWok;

        }

        public async Task<int> AddEntity(ContractDetails entity)
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

        public IEnumerable<ContractDetails> Find(Expression<Func<ContractDetails, bool>> expression)
        {
            return _repository.Find(expression);
        }

        public async Task<IEnumerable<ContractDetails>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ContractDetails> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> IsRecordExistsAsync(Expression<Func<ContractDetails, bool>> expression)
        {
            return await _repository.IsRecordExistsAsync(expression);
        }

        public async Task<bool> UpdateEntity(ContractDetails entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
