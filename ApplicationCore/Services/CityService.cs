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
    public class CityService : ICityService
    {
        private readonly IRepository<City> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public CityService(IRepository<City> repository, IUnitOfWok unitOfWok)
        {
            _repository = repository;
            _unitOfWok = unitOfWok;
        }
        public async Task<int> AddEntity(City entity)
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

        public IEnumerable<City> Find(Expression<Func<City, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<City> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateEntity(City entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
