using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWok
    {
        private readonly AppDbContext _dbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            this._dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            this._dbContext.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            this._dbContext.Database.RollbackTransaction();
        }

        public async Task SaveChangesAsync()
        {
            await this._dbContext.SaveChangesAsync(); 
        }
    }
}
