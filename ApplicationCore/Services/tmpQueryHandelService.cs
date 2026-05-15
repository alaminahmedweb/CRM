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
    public class tmpQueryHandelService : ItmpQueryHandelService
    {
        private readonly IRepository<tmpQueryHandel> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public tmpQueryHandelService(IRepository<tmpQueryHandel> repository, 
            IUnitOfWok unitOfWok)
        {
            _repository = repository;
            _unitOfWok = unitOfWok;
        }

        public Task<int> AddEntity(tmpQueryHandel entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<tmpQueryHandel> Find(Expression<Func<tmpQueryHandel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<tmpQueryHandel>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<tmpQueryHandel> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<tmpQueryHandel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(tmpQueryHandel entity)
        {
            throw new NotImplementedException();
        }
    }
}
