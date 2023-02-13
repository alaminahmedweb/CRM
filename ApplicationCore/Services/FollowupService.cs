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
    public class FollowupService : IFollowupService
    {
        private readonly IRepository<Followup> _repository;
        private readonly IUnitOfWok _unitOfWok;
        public FollowupService(IRepository<Followup> repository, IUnitOfWok unitOfWok)
        {
            _repository = repository;
            _unitOfWok = unitOfWok; 
        }
        public async Task<int> AddEntity(Followup followup)
        {
            await _repository.AddEntity(followup);
            await _unitOfWok.SaveChangesAsync();
            return followup.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
           bool success= await _repository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return success;
        }

        public IEnumerable<Followup> Find(Expression<Func<Followup, bool>> expression)
        {
            var entity = _repository.Find(expression);
            return entity;
        }

        public async Task<IEnumerable<Followup>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Followup> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public IEnumerable<Followup> GetFollowupsByDate(DateTime date)
        {
            var entity= Find(Followup=>Followup.FollowupCallDate==date);
            return entity;
            //return await _repository.GetByIdAsync(date);
        }

        public async Task<bool> UpdateEntity(Followup followup)
        {
            bool success=await _repository.UpdateEntity(followup);
            await _unitOfWok.SaveChangesAsync();
            return success;
        }
    }
}
