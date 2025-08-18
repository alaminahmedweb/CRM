using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class ChartOfAccountService : IChartOfAccountService
    {
        private readonly IRepository<ChartOfAccount> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public ChartOfAccountService(IRepository<ChartOfAccount> repository, IUnitOfWok unitOfWok)
        {
            _repository = repository;
            _unitOfWok = unitOfWok;
        }


        public async Task<int> AddEntity(ChartOfAccount entity)
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

        public IEnumerable<ChartOfAccount> Find(Expression<Func<ChartOfAccount, bool>> expression)
        {
            return _repository.Find(expression);
        }

        public async Task<IEnumerable<ChartOfAccount>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ChartOfAccount> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> IsRecordExistsAsync(Expression<Func<ChartOfAccount, bool>> expression)
        {
            return await _repository.IsRecordExistsAsync(expression);
        }

        public async Task<bool> UpdateEntity(ChartOfAccount entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
