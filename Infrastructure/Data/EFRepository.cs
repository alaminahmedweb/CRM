using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        public readonly AppDbContext _dbContext;
        internal DbSet<T> DbSet { get; set; }

        public EFRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            this.DbSet = dbContext.Set<T>();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.DbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<int> AddEntity(T entity)
        {
            try
            {
                await this.DbSet.AddAsync(entity);
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public virtual async Task<bool> DeleteEntity(object id)
        {
            T entity = await DbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            else
            {
                DbSet.Remove(entity);
                return true;
            }
        }

        public virtual async Task<bool> UpdateEntity(T entity)
        {
            DbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return  _dbContext.Set<T>().Where(expression);
        }
    }
}
