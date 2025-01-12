using ApplicationCore.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IMISReportQueryService
    {
        IEnumerable<AllCustomerListDto> GetAllCustomersByDate(DateTime dateFrom, DateTime dateTo,int employeeId,int contactId,int categoryId);
        IEnumerable<CustomerDto> GetAllFollowupDoneListByDate(DateTime dateFrom, DateTime dateTo);
        List<BookingItemDto> GetEntryDateWiseBookingList(DateTime dateFrom, DateTime dateTo);
        List<BookingItemDto> GetBookingCancelAndShiftList(DateTime dateFrom, DateTime dateTo,string status);
        IEnumerable<CustomerCountDto> GetContactWiseCustomersListByDate(DateTime dateFrom, DateTime dateTo);
        IEnumerable<CustomerCountDto> GetStatusWiseCustomersListByDate(DateTime dateFrom, DateTime dateTo);
        IEnumerable<CustomerCountDto> GetStatusWiseFollowupDoneListByDate(DateTime dateFrom, DateTime dateTo);
    }
}
