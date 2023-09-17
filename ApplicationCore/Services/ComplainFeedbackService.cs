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
    public class ComplainFeedbackService : IComplainFeedbackService
    {
        private readonly IRepository<ComplainFeedback> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public ComplainFeedbackService(IRepository<ComplainFeedback> repository,IUnitOfWok unitOfWok)
        {
            this._repository = repository;
            this._unitOfWok = unitOfWok;
        }
        public async Task<int> AddEntity(ComplainFeedback entity)
        {
            try
            {
                await _repository.AddEntity(entity);
                await _unitOfWok.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<bool> DeleteEntity(object id)
        {
            await _repository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }

        public IEnumerable<ComplainFeedback> Find(Expression<Func<ComplainFeedback, bool>> expression)
        {
            return _repository.Find(expression);
        }

        public async Task<IEnumerable<ComplainFeedback>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ComplainFeedback> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<ComplainFeedback, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEntity(ComplainFeedback entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;

        }
    }
}
