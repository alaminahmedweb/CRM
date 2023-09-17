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
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;
        private readonly IUnitOfWok _unitOfWok;
        public EmployeeService(IRepository<Employee> employeeRepository, IUnitOfWok unitOfWok)
        {
            _repository = employeeRepository;
            _unitOfWok = unitOfWok;
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Employee> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<int> AddEntity(Employee mpo)
        {
            await _repository.AddEntity(mpo);
            await _unitOfWok.SaveChangesAsync();
            return mpo.Id;
        }
        public async Task<bool> UpdateEntity(Employee mpo)
        {
            bool isSuccess= await _repository.UpdateEntity(mpo);
            await _unitOfWok.SaveChangesAsync();
            return isSuccess;
        }
        public async Task<bool> DeleteEntity(object id)
        {
            bool isSuccess= await _repository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return isSuccess;
        }

        public IEnumerable<Employee> Find(Expression<Func<Employee, bool>> expression)
        {
            return  _repository.Find(expression);
        }

        public async Task<bool> IsRecordExistsAsync(Expression<Func<Employee, bool>> expression)
        {
            return await _repository.IsRecordExistsAsync(expression);
        }
    }
}
