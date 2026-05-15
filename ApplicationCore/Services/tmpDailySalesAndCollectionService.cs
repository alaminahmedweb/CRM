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
    public class tmpDailySalesAndCollectionService : ItmpDailySalesAndCollectionService
    {
        private readonly IRepository<tmpDailySalesAndCollection> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public tmpDailySalesAndCollectionService(IRepository<tmpDailySalesAndCollection> tmpDailySalesAndCollection,
            IUnitOfWok unitOfWok)
        {
            _repository = tmpDailySalesAndCollection;
            _unitOfWok = unitOfWok;
        }

        public Task<int> AddEntity(tmpDailySalesAndCollection entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tmpDailySalesAndCollection> Find(Expression<Func<tmpDailySalesAndCollection, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<tmpDailySalesAndCollection>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<tmpDailySalesAndCollection> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<tmpDailySalesAndCollection, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(tmpDailySalesAndCollection entity)
        {
            throw new NotImplementedException();
        }
    }
}
