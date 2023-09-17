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
    public class ComplainRegisterService : IComplainRegisterService
    {
        private readonly IRepository<ComplainRegister> _repository;
        private readonly IUnitOfWok _unitOfWok;

        public ComplainRegisterService(IRepository<ComplainRegister> repository,IUnitOfWok unitOfWok)
        {
            this._repository = repository;
            this._unitOfWok = unitOfWok;
        }
        public async Task<int> AddEntity(ComplainRegister entity)
        {
            try
            {
                await _repository.AddEntity(entity);
                await _unitOfWok.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception)
            {
                //throw;
                return 0;
            }
        }

        public async Task<bool> DeleteEntity(object id)
        {
            try
            {
                await _repository.DeleteEntity(id);
                await _unitOfWok.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {
                //throw;
                return false;
            }
        }

        public IEnumerable<ComplainRegister> Find(Expression<Func<ComplainRegister, bool>> expression)
        {
            return _repository.Find(expression);    
        }

        public async Task<IEnumerable<ComplainRegister>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ComplainRegister> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<ComplainRegister, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateEntity(ComplainRegister entity)
        {
            try
            {
                await _repository.UpdateEntity(entity);
                await _unitOfWok.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }
    }
}
