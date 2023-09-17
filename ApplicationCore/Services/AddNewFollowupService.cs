using ApplicationCore.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using System.Linq.Expressions;
using AutoMapper;
using ApplicationCore.Entities;

namespace ApplicationCore.Services
{
    public class AddNewFollowupService : IAddNewFollowupService
    {
        private readonly IRepository<AddFollowupDto> _followupRepository;
        private readonly IFollowupService _followupService;
        private readonly IUnitOfWok _unitOfWok;
        private readonly IMapper _mapper;

        public AddNewFollowupService(IRepository<AddFollowupDto> followupRepository, 
            IFollowupService followupService,
            IUnitOfWok unitOfWok,
            IMapper mapper)
        {
            _followupRepository = followupRepository;
            this._followupService = followupService;
            _unitOfWok = unitOfWok;
            this._mapper = mapper;
        }
        public async Task<int> AddEntity(AddFollowupDto entity)
        {
            _unitOfWok.BeginTransaction();
            try
            {
                var entityToUpdate = await _followupService.GetByIdAsync(entity.FollowupId);
                entityToUpdate.IsFollowupDone = true;
                await _followupService.UpdateEntity(entityToUpdate);

                var entityToAdd = _mapper.Map<AddFollowupDto, Followup>(entity);
                await _followupService.AddEntity(entityToAdd);

                _unitOfWok.CommitTransaction();
                return entityToAdd.Id;
            }
            catch (Exception ex)
            {
                _unitOfWok.RollbackTransaction();
                return 0;
            }
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AddFollowupDto> Find(Expression<Func<AddFollowupDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AddFollowupDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AddFollowupDto> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<AddFollowupDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(AddFollowupDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
