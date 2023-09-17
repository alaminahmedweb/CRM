using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IFollowupQueryService
    {
        IEnumerable<CustomerDto> GetDailyFollowupListByMpoIdAndFollowupType(DateTime dateFrom, DateTime dateTo, int mpoId,string followupType);
        FollowupDetailsByIdDto GetFollowupDetailsByCustId(int customerId);
        CustomerFollowupDto GetCustomerDetailsByFollowupId(int followupId);
        Task<bool> IsAlreadyGivenFollowup(int customerId, DateTime followupDate);
        Task<Followup> GetFollowupByIdAsync(int id);
        int GetMaxFollowupIdByCustId(int customerId);

    }
}
