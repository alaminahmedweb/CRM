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
    public class MonthListService : IMonthListService
    {
        private readonly IRepository<MonthList> _repository;

        public MonthListService(IRepository<MonthList> repository)
        {
            this._repository = repository;
        }
        public Task<int> AddEntity(MonthList entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MonthList> Find(Expression<Func<MonthList, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MonthList>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<MonthList> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<MonthList, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(MonthList entity)
        {
            throw new NotImplementedException();
        }
    }
}
