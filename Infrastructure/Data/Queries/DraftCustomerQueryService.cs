using ApplicationCore.DtoModels;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries
{
    public class DraftCustomerQueryService : IDraftCustomerQueryService
    {
        private readonly AppDbContext _dbContext;

        public DraftCustomerQueryService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public IEnumerable<DraftCustomerDto> GetDraftCustomer(DateTime dateFrom, DateTime dateTo)
        {
            var result = from cus in _dbContext.DraftCustomer.Where(a=>a.NextFollowupDate>=dateFrom && 
                                        a.NextFollowupDate<=dateTo 
                                        && a.IsFollowupDone == false)
                         join con in _dbContext.Contacts on cus.ContactId equals con.Id
                         select new
                         {
                             ClientName = cus.ClientName,
                             Remarks = cus.Remarks,
                             Id = cus.Id,
                             NextFollowupDate = cus.NextFollowupDate,
                             MobileNo = cus.MobileNo,
                             IsFollowupDone=cus.IsFollowupDone,
                             ContactName = con.Name
                         };

            List<DraftCustomerDto> allCustomerList = new List<DraftCustomerDto>();

            foreach (var data in result)
            {
                DraftCustomerDto customer = new DraftCustomerDto();
                customer.ClientName = data.ClientName;
                customer.Remarks = data.Remarks;
                customer.Id = data.Id;
                customer.NextFollowupDate = data.NextFollowupDate;
                customer.MobileNo = data.MobileNo;
                customer.IsFollowupDone = data.IsFollowupDone;
                customer.ContactName = data.ContactName;

                allCustomerList.Add(customer);

            }

            return allCustomerList;
        }

    }
}
