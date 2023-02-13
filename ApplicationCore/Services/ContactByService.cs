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
        private readonly IRepository<ContactBy> _contactByRepository;
        private readonly IUnitOfWok _unitOfWok;
        public ContactByService(IRepository<ContactBy> contactByRepository, IUnitOfWok unitOfWok)
        {
            _contactByRepository = contactByRepository;
            _unitOfWok = unitOfWok;
        }

        public async Task<int> AddEntity(ContactBy contact)
        {
            await _contactByRepository.AddEntity(contact);
            await _unitOfWok.SaveChangesAsync();
            return contact.Id;
        }

        public async Task<bool> DeleteEntity(object id)
        {
            await _contactByRepository.DeleteEntity(id);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }

        public IEnumerable<ContactBy> Find(Expression<Func<ContactBy, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ContactBy>> GetAllAsync()
        {
            return await _contactByRepository.GetAllAsync();
        }

        public async Task<ContactBy> GetByIdAsync(object id)
        {
            return await _contactByRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateEntity(ContactBy contact)
        {
            await _contactByRepository.UpdateEntity(contact);
            await _unitOfWok.SaveChangesAsync();
            return true;
        }
    }
}
