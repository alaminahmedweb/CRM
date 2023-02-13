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
        private readonly IRepository<Team> _teamRepository;
        private readonly IUnitOfWok _unitOfWok;
        public TeamService(IRepository<Team> teamRepository, IUnitOfWok unitOfWok)
        {
            _teamRepository = teamRepository;
            _unitOfWok = unitOfWok;
        }
        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _teamRepository.GetAllAsync();
        }

        public async Task<Team> GetByIdAsync(object id)
        {
            return await _teamRepository.GetByIdAsync(id);
        }

        public async Task<int> AddEntity(Team team)
        {
            await _teamRepository.AddEntity(team);
            await _unitOfWok.SaveChangesAsync();
            return team.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            bool isSuccess = await _teamRepository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateEntity(Team team)
        {
            bool isSuccess = await _teamRepository.UpdateEntity(team);
            await _unitOfWok.SaveChangesAsync();
            return isSuccess;
        }

        public IEnumerable<Team> Find(Expression<Func<Team, bool>> expression)
        {
            return _teamRepository.Find(expression);
        }
    }
}
