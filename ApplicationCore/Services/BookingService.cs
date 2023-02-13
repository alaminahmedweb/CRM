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
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public BookingService(IRepository<Booking> repository,IUnitOfWok unitOfWok)
        {
            this._repository = repository;
            this._unitOfWok = unitOfWok;    
        }
        public async Task<int> AddEntity(Booking entity)
        {
            await _repository.AddEntity(entity);
            await _unitOfWok.SaveChangesAsync();
            return entity.Id;
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> Find(Expression<Func<Booking, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(Booking entity)
        {
            throw new NotImplementedException();
        }
    }
}
