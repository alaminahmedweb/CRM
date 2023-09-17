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
    public class AddComplainFeebackService : IAddComplainFeebackService
    {
        private readonly IUnitOfWok _unitOfWok;
        private readonly IComplainFeedbackService _complainFeedbackService;
        private readonly IComplainRegisterService _complainRegisterService;
        private readonly IMapper _mapper;

        public AddComplainFeebackService(IUnitOfWok unitOfWok,
            IComplainFeedbackService complainFeedbackService,
            IComplainRegisterService complainRegisterService,
            IMapper mapper)
        {
            this._unitOfWok = unitOfWok;
            this._complainFeedbackService = complainFeedbackService;
            this._complainRegisterService = complainRegisterService;
            this._mapper = mapper;
        }

        public async Task<int> AddEntity(ComplainFeedbackDto entity)
        {
            _unitOfWok.BeginTransaction();
            try
            {
                var entityToUpdate = await _complainRegisterService.GetByIdAsync(entity.ComplainId);
                entityToUpdate.IsGivenFeedback = true;
                await _complainRegisterService.UpdateEntity(entityToUpdate);

                var entityToAdd = _mapper.Map<ComplainFeedbackDto, ComplainFeedback>(entity);
                await _complainFeedbackService.AddEntity(entityToAdd);

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
        public IEnumerable<ComplainFeedbackDto> Find(Expression<Func<ComplainFeedbackDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ComplainFeedbackDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ComplainFeedbackDto> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<ComplainFeedbackDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(ComplainFeedbackDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
