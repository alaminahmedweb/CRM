using ApplicationCore.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IFeedbackQueryService
    {
        List<ServiceFeedbackDto> GetBookingList(DateTime date);
        ServiceFeedbackDto GetBookingListByid(int id);
        List<ComplainFeedbackDto> GetDailyComplainList(DateTime date);
        ComplainFeedbackDto GetComplainDetailsById(int id);
    }
}
