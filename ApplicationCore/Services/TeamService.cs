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
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _repository;
        private readonly IUnitOfWok _unitOfWok;
        public TeamService(IRepository<Team> teamRepository, IUnitOfWok unitOfWok)
        {
            _repository = teamRepository;
            _unitOfWok = unitOfWok;
        }
        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Team> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> AddEntity(Team team)
        {
            await _repository.AddEntity(team);
            await _unitOfWok.SaveChangesAsync();
            return team.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            bool isSuccess = await _repository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateEntity(Team team)
        {
            bool isSuccess = await _repository.UpdateEntity(team);
            await _unitOfWok.SaveChangesAsync();
            return isSuccess;
        }

        public IEnumerable<Team> Find(Expression<Func<Team, bool>> expression)
        {
            return _repository.Find(expression);
        }

        public async Task<bool> IsRecordExistsAsync(Expression<Func<Team, bool>> expression)
        {
            return await _repository.IsRecordExistsAsync(expression);
        }
    }
}
