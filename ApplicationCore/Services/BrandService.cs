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
    public class BrandService : IBrandService
    {
        private readonly IRepository<Brand> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public BrandService(IRepository<Brand> repository, IUnitOfWok unitOfWok)
        {
            this._repository = repository;
            this._unitOfWok = unitOfWok;
        }
        public async Task<int> AddEntity(Brand entity)
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

        public IEnumerable<Brand> Find(Expression<Func<Brand, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Brand> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateEntity(Brand entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
