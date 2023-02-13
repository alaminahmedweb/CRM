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
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IUnitOfWok _unitOfWok;
        public EmployeeService(IRepository<Employee> employeeRepository, IUnitOfWok unitOfWok)
        {
            _employeeRepository = employeeRepository;
            _unitOfWok = unitOfWok;
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }
        public async Task<Employee> GetByIdAsync(object id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }
        public async Task<int> AddEntity(Employee mpo)
        {
            await _employeeRepository.AddEntity(mpo);
            await _unitOfWok.SaveChangesAsync();
            return mpo.Id;
        }
        public async Task<bool> UpdateEntity(Employee mpo)
        {
            bool isSuccess= await _employeeRepository.UpdateEntity(mpo);
            await _unitOfWok.SaveChangesAsync();
            return isSuccess;
        }
        public async Task<bool> DeleteEntity(object id)
        {
            bool isSuccess= await _employeeRepository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return isSuccess;
        }

        public IEnumerable<Employee> Find(Expression<Func<Employee, bool>> expression)
        {
            throw new NotImplementedException();
        }

    }
}
