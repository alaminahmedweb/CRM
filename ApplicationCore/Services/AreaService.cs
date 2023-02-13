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
        private readonly IRepository<Area> _areaRepository;
        private readonly IUnitOfWok _unitOfWok;
        public AreaService(IRepository<Area> areaRepository, IUnitOfWok unitOfWok)
        {
            _areaRepository = areaRepository;
            _unitOfWok = unitOfWok; 
        }

        public async Task<int> AddEntity(Area area)
        {
            await _areaRepository.AddEntity(area);
            await _unitOfWok.SaveChangesAsync();
            return area.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            await _areaRepository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Area> Find(Expression<Func<Area, bool>> expression)
        {
            return _areaRepository.Find(expression);
        }

        public async Task<IEnumerable<Area>> GetAllAsync()
        {
            return await _areaRepository.GetAllAsync();
        }

        public async Task<Area> GetByIdAsync(object id)
        {
            return await _areaRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateEntity(Area area)
        {
            await _areaRepository.UpdateEntity(area);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
