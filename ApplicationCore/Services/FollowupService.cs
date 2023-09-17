using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public FollowupService(IRepository<Followup> repository, 
            IUnitOfWok unitOfWok,
            IMapper mapper)
        {
            this._repository = repository;
            this._unitOfWok = unitOfWok;
            this._mapper = mapper;
        }

        public async Task<int> AddEntity(Followup followup)
        {
            await _repository.AddEntity(followup);
            await _unitOfWok.SaveChangesAsync();
            return followup.Id;// followup.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            await _repository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }

        public IEnumerable<Followup> Find(Expression<Func<Followup, bool>> expression)
        {
            var entity =  _repository.Find(expression);
            return entity;
        }

        public async Task<IEnumerable<Followup>> GetAllAsync()
        {
            return await _repository.GetAllAsync(); 
        }

        public async Task<Followup> GetByIdAsync(object id)
        {
            return await this._repository.GetByIdAsync(id); 
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<Followup, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEntity(Followup entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
