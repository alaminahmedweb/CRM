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
    public class AreaService : IAreaService
    {
        private readonly IRepository<Area> _repository;
        private readonly IUnitOfWok _unitOfWok;
        public AreaService(IRepository<Area> areaRepository, IUnitOfWok unitOfWok)
        {
            _repository = areaRepository;
            _unitOfWok = unitOfWok; 
        }

        public async Task<int> AddEntity(Area area)
        {
            await _repository.AddEntity(area);
            await _unitOfWok.SaveChangesAsync();
            return area.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            await _repository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Area> Find(Expression<Func<Area, bool>> expression)
        {
            return _repository.Find(expression);
        }

        public async Task<IEnumerable<Area>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Area> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> IsRecordExistsAsync(Expression<Func<Area, bool>> expression)
        {
            return await _repository.IsRecordExistsAsync(expression);
        }

        public async Task<bool> UpdateEntity(Area area)
        {
            await _repository.UpdateEntity(area);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
