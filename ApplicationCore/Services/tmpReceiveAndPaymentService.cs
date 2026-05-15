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
    public class tmpReceiveAndPaymentService : ItmpReceiveAndPaymentService
    {
        private readonly IRepository<tmpReceiveAndPayment> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public tmpReceiveAndPaymentService(IRepository<tmpReceiveAndPayment> tmpReceiveAndPaymentRepository, 
            IUnitOfWok unitOfWok)
        {
            _repository = tmpReceiveAndPaymentRepository;
            _unitOfWok = unitOfWok;

        }

        public Task<int> AddEntity(tmpReceiveAndPayment entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tmpReceiveAndPayment> Find(Expression<Func<tmpReceiveAndPayment, bool>> expression)
        {
            return _repository.Find(expression);
        }

        public async Task<IEnumerable<tmpReceiveAndPayment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<tmpReceiveAndPayment> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<tmpReceiveAndPayment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(tmpReceiveAndPayment entity)
        {
            throw new NotImplementedException();
        }
    }
}
