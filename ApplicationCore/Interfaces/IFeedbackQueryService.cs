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
        List<ServiceFeedbackDto> GetBookingList(DateTime dateFrom, DateTime dateTo);
        ServiceFeedbackDto GetBookingListByid(int id);
        List<ComplainFeedbackDto> GetDailyComplainList(DateTime dateFrom, DateTime dateTo);
        ComplainFeedbackDto GetComplainDetailsById(int id);
        bool IsAlreadyGivenFeedback(int bookingId);
        bool IsAlreadyGivenComplain(int bookingId);
        bool IsAlreadyGivenComplainFeedback(int complainId);
    }
}
