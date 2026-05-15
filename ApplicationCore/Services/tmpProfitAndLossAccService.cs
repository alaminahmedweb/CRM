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
    public class tmpProfitAndLossAccService : ItmpProfitAndLossAccService
    {
        private readonly IRepository<tmpProfitAndLossAcc> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public tmpProfitAndLossAccService(IRepository<tmpProfitAndLossAcc> tmpProfitAndLossAcc,
            IUnitOfWok unitOfWok)
        {
            _repository = tmpProfitAndLossAcc;
            _unitOfWok = unitOfWok;
        }

        public Task<int> AddEntity(tmpProfitAndLossAcc entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tmpProfitAndLossAcc> Find(Expression<Func<tmpProfitAndLossAcc, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<tmpProfitAndLossAcc>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<tmpProfitAndLossAcc> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<tmpProfitAndLossAcc, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(tmpProfitAndLossAcc entity)
        {
            throw new NotImplementedException();
        }
    }
}
