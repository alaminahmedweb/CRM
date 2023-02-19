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
    public class FeedbackService : IFeedbackService
    {
        private readonly IRepository<ServiceFeedback> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public FeedbackService(IRepository<ServiceFeedback> repository,
            IUnitOfWok unitOfWok)
        {
            this._repository = repository;
            this._unitOfWok = unitOfWok;
        }
        public async Task<int> AddEntity(ServiceFeedback entity)
        {
            await _repository.AddEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            await this._repository.DeleteEntity(id);
            return true;
        }

        public IEnumerable<ServiceFeedback> Find(Expression<Func<ServiceFeedback, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ServiceFeedback>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<ServiceFeedback> GetByIdAsync(object id)
        {
            return this._repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateEntity(ServiceFeedback entity)
        {
            await _repository.UpdateEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
