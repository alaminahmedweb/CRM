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
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IRepository<ServiceType> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public ServiceTypeService(IRepository<ServiceType> repository, IUnitOfWok unitOfWok)
        {
            _repository = repository;
            _unitOfWok = unitOfWok;
        }
        public async Task<int> AddEntity(ServiceType entity)
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

        public IEnumerable<ServiceType> Find(Expression<Func<ServiceType, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ServiceType>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ServiceType> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateEntity(ServiceType entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
