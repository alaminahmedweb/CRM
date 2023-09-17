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
    public class ContactByService : IContactByService
    {
        private readonly IRepository<ContactBy> _repository;
        private readonly IUnitOfWok _unitOfWok;
        public ContactByService(IRepository<ContactBy> contactByRepository, IUnitOfWok unitOfWok)
        {
            _repository = contactByRepository;
            _unitOfWok = unitOfWok;
        }

        public async Task<int> AddEntity(ContactBy contact)
        {
            await _repository.AddEntity(contact);
            await _unitOfWok.SaveChangesAsync();
            return contact.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            await _repository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }

        public IEnumerable<ContactBy> Find(Expression<Func<ContactBy, bool>> expression)
        {
            return  _repository.Find(expression);
        }

        public async Task<IEnumerable<ContactBy>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ContactBy> GetByIdAsync(object id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> IsRecordExistsAsync(Expression<Func<ContactBy, bool>> expression)
        {
            return await _repository.IsRecordExistsAsync(expression);
        }

        public async Task<bool> UpdateEntity(ContactBy contact)
        {
            await _repository.UpdateEntity(contact);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
