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
    public class tmpCustomerListService : ItmpCustomerListService
    {

        private readonly IRepository<tmpCustomerList> _repository;
        private readonly IUnitOfWok _unitOfWok;


        public tmpCustomerListService(IRepository<tmpCustomerList> tmpCustomerListRepository,
            IUnitOfWok unitOfWok)
        {
            _repository = tmpCustomerListRepository;
            _unitOfWok = unitOfWok;

        }
        public Task<int> AddEntity(tmpCustomerList entity)
        {
            throw new NotImplementedException();
        }


        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tmpCustomerList> Find(Expression<Func<tmpCustomerList, bool>> expression)
        {
            return _repository.Find(expression);
        }

        public async Task<IEnumerable<tmpCustomerList>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<tmpCustomerList> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<tmpCustomerList, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(tmpCustomerList entity)
        {
            throw new NotImplementedException();
        }
    }
}
