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
    public class DesignationService : IDesignationService
    {
        private readonly IRepository<Designation> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public DesignationService(IRepository<Designation> repository, IUnitOfWok unitOfWok)
        {
            this._repository = repository;
            this._unitOfWok = unitOfWok;
        }
        public async Task<int> AddEntity(Designation entity)
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

        public IEnumerable<Designation> Find(Expression<Func<Designation, bool>> expression)
        {
            return _repository.Find(expression);
        }

        public async Task<IEnumerable<Designation>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Designation> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> IsRecordExistsAsync(Expression<Func<Designation, bool>> expression)
        {
            return await _repository.IsRecordExistsAsync(expression);
        }

        public async Task<bool> UpdateEntity(Designation entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;

        }
    }
}
