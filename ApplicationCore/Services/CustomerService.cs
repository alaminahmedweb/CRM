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
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IUnitOfWok _unitOfWok;
        public CustomerService(IRepository<Customer> repository, IUnitOfWok unitOfWok)
        {
            _repository = repository;
            _unitOfWok = unitOfWok;
        }
        public async Task<int> AddEntity(Customer customer)
        {
            await _repository.AddEntity(customer);
            await _unitOfWok.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
             await _repository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Customer> GetByIdAsync(object id)
        {
            return _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateEntity(Customer customer)
        {
            await _repository.UpdateEntity(customer);
            await _unitOfWok.SaveChangesAsync();
            return true;

        }
    }
}
