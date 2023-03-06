using ApplicationCore.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IFollowupQueryService
    {
        IEnumerable<CustomerDto> GetDailyFollowupList(DateTime date);
        FollowupDetailsByIdDto GetFollowupDetailsByCustId(int id);
        CustomerFollowupDto GetCustomerDetailsByCustId(int id);
    }
}
