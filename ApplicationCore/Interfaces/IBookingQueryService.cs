using ApplicationCore.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBookingQueryService
    {

        List<BookingItemDto> GetBookingDetailsByDate(DateTime dateFrom, DateTime dateTo);
        BookingDto GetCustomerAndBookingDetailsByBookingId(int bookingid);
        bool IsBookedAlready(int teamId, int shiftId, DateTime bookingDate); 
    }
}
