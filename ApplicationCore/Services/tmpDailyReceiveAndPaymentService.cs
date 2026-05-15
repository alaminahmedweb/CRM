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
    public class tmpDailyReceiveAndPaymentService : ItmpDailyReceiveAndPaymentService
    {
        private readonly IRepository<tmpDailyReceiveAndPayment> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public tmpDailyReceiveAndPaymentService(IRepository<tmpDailyReceiveAndPayment> tmpDailyReceiveAndPaymentRepository,
         IUnitOfWok unitOfWok)
        {
            _repository = tmpDailyReceiveAndPaymentRepository;
            _unitOfWok = unitOfWok;
        }

        public Task<int> AddEntity(tmpDailyReceiveAndPayment entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tmpDailyReceiveAndPayment> Find(Expression<Func<tmpDailyReceiveAndPayment, bool>> expression)
        {
            return _repository.Find(expression);
        }

        public async Task<IEnumerable<tmpDailyReceiveAndPayment>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<tmpDailyReceiveAndPayment> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<tmpDailyReceiveAndPayment, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(tmpDailyReceiveAndPayment entity)
        {
            throw new NotImplementedException();
        }
    }
}
