using ApplicationCore.DtoModels;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries
{
    public class DashboardQueryService : IDashboardQueryService
    {
        private readonly AppDbContext _dbContext;

        public DashboardQueryService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public DashboardDto GetDashboardData()
        {


            DashboardDto dashboardDto = new DashboardDto();
            DateTime currentDateFrom = (TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time"));

            //dashboardDto.TodayTotalFollowupQty = 1.ToString();
            //dashboardDto.TotalCustomer = 1.ToString();
            //dashboardDto.TodayNewCustomer = 1.ToString();
            //dashboardDto.TodayFollowupDoneQty = 1.ToString();
            //dashboardDto.TodayRemainingFollowupQty = 1.ToString();
            //dashboardDto.TodayBookingQty = 1.ToString();
            //dashboardDto.TodayBookingAmount = 1.ToString();
            //dashboardDto.ThisMonthBookingQty =1.ToString();
            //dashboardDto.ThisMonthComplainPendingQty = 1.ToString();
            //dashboardDto.ThisMonthBookingAmount = 1.ToString();

            dashboardDto.TotalCustomer = _dbContext.Customers.Count().ToString();

            dashboardDto.TodayNewCustomer = _dbContext.Customers.Where(a => a.ModifiedDate.Date == currentDateFrom.Date).Count().ToString();

            //dashboardDto.TodayTotalFollowupQty = (from fol in _dbContext.Followups
            //                    .Where(a => a.FollowupCallDate.Date == currentDateFrom.Date && a.Status != "Inactive")
            //                                      select fol).Count().ToString();

            //dashboardDto.TodayFollowupDoneQty = (from fol in _dbContext.Followups
            //                    .Where(a => a.FollowupCallDate.Date == currentDateFrom.Date && a.IsFollowupDone == true && a.Status != "Inactive")
            //                                     select fol).Count().ToString();

            //dashboardDto.TodayRemainingFollowupQty = (from fol in _dbContext.Followups
            //        .Where(a => a.FollowupCallDate.Date == currentDateFrom.Date && a.IsFollowupDone == false && a.Status != "Inactive")
            //                                          select fol).Count().ToString();

            dashboardDto.TodayBookingQty = (from bk in _dbContext.Bookings
                    .Where(a => a.EntryDate.Date == currentDateFrom.Date).Where(a => a.Status != "Cancel")
                                            select bk).Count().ToString();

            //dashboardDto.TodayBookingAmount =
            //            (from bk in _dbContext.Bookings
            //             join fol in _dbContext.Followups
            //             on bk.FollowupId equals fol.Id
            //             where bk.EntryDate.Date == currentDateFrom.Date
            //             && bk.Status != "Cancel"
            //             select fol.AgreeAmount).Sum().ToString();

            var customerCount = from cus in _dbContext.Customers
                                join con in _dbContext.Contacts
                                on cus.ContactId equals con.Id
                                where cus.ModifiedDate.Date == currentDateFrom.Date
                                group cus by con.Name into g
                                select new
                                {
                                    ContactName = g.Key,
                                    TotalCount = g.Count()
                                };
            foreach (var item in customerCount)
            {
                CustomerCountDto customerCountDto = new CustomerCountDto();
                customerCountDto.TotalCount = item.TotalCount;
                customerCountDto.GroupName = item.ContactName;
                dashboardDto.TodayContactWiseCustomerList.Add(customerCountDto);
            }


            DateTime now = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "Bangladesh Standard Time");
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            dashboardDto.ThisMonthBookingQty = (from bk in _dbContext.Bookings
                    .Where(a => a.BookingDate.Date >= startDate.Date && a.BookingDate <= endDate.Date).Where(a => a.Status != "Cancel")
                                                select bk).Count().ToString();

            //dashboardDto.ThisMonthBookingAmount =
            //            (from bk in _dbContext.Bookings
            //             join fol in _dbContext.Followups
            //             on bk.FollowupId equals fol.Id
            //             where bk.BookingDate.Date >= startDate.Date && bk.BookingDate <= endDate.Date
            //             && bk.Status != "Cancel"
            //             select fol.AgreeAmount).Sum().ToString();

            dashboardDto.ThisMonthComplainPendingQty = (from compl in _dbContext.Complains
                    .Where(a => a.ComplainDate.Date >= startDate.Date && a.ComplainDate <= endDate.Date).Where(a => a.IsGivenFeedback == false)
                                                        select compl).Count().ToString();


            var query = _dbContext.Bookings.Where(a=>a.Status!="Cancel")
                .Where(b => b.BookingDate.Year == DateTime.Now.Date.Year)
                .GroupBy(b => new
                {
                    MonthNumber = b.BookingDate.Month
                })
                .OrderBy(g => g.Key.MonthNumber)
                .Select(g => new
                {
                    BookingMonthNo = g.Key.MonthNumber,
                    BookingMonthName = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key.MonthNumber),
                    TotalBookings = g.Count()
                })
                .ToList();


            dashboardDto.PieLabels = new List<string>();//new string[11];
            dashboardDto.PieData = new List<string>();// new string[11];

            int i = 0;

            foreach (var item in query)
            {
                dashboardDto.PieLabels.Add(item.BookingMonthName);
                dashboardDto.PieData.Add(item.TotalBookings.ToString());
                i++;
            }

            return dashboardDto;
        }
    }
}
