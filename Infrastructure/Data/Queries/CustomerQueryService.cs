using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries
{
    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly AppDbContext _dbContext;

        public CustomerQueryService(AppDbContext appDbContext) 
        {
            this._dbContext = appDbContext;
        }
        public bool IsMobileNoAlreadyExists(string mobileNo)
        {
            bool isExists = _dbContext.ContractDetails.Any(a => a.MobileNo == mobileNo);
            return isExists;
        }
    }
}
