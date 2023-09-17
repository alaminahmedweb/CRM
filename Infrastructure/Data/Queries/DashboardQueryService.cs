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

            dashboardDto.TodayTotalFollowupQty = (from fol in _dbContext.Followups
                                .Where(a => a.FollowupCallDate.Date == DateTime.Now.Date && a.Status != "Inactive")
                                                  select fol).Count().ToString();

            dashboardDto.TodayFollowupDoneQty = (from fol in _dbContext.Followups
                                .Where(a => a.FollowupCallDate.Date == DateTime.Now.Date && a.IsFollowupDone == true && a.Status != "Inactive")
                                                 select fol).Count().ToString();

            dashboardDto.TodayRemainingFollowupQty = (from fol in _dbContext.Followups
                    .Where(a => a.FollowupCallDate.Date == DateTime.Now.Date && a.IsFollowupDone == false && a.Status != "Inactive")
                                                      select fol).Count().ToString();

            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            dashboardDto.ThisMonthBookingQty = (from bk in _dbContext.Bookings
                    .Where(a => a.BookingDate.Date >= startDate.Date && a.BookingDate <= endDate.Date).Where(a => a.Status != "Cancel")
                                                select bk).Count().ToString();

            dashboardDto.ThisMonthBookingAmount =
                        (from bk in _dbContext.Bookings
                         join fol in _dbContext.Followups
                         on bk.FollowupId equals fol.Id
                         where bk.BookingDate.Date >= startDate.Date && bk.BookingDate <= endDate.Date
                         && bk.Status != "Cancel"
                         select fol.AgreeAmount).Sum().ToString();

            dashboardDto.ThisMonthComplainPendingQty = (from compl in _dbContext.Complains
                    .Where(a => a.ComplainDate.Date >= startDate.Date && a.ComplainDate <= endDate.Date).Where(a=>a.IsGivenFeedback==false)
                                                select compl).Count().ToString();

            var customerCount = from cus in _dbContext.Customers
                                join con in _dbContext.Contacts
                                on cus.ContactId equals con.Id
                                where cus.ModifiedDate.Date >= startDate.Date && cus.ModifiedDate <= endDate.Date
                                group cus by con.Name into g
                                select new
                                {
                                    ContactName = g.Key,
                                    TotalCount = g.Count()
                                };
            foreach(var item in customerCount)
            {
                CustomerCountDto customerCountDto = new CustomerCountDto();
                customerCountDto.TotalCount = item.TotalCount;
                customerCountDto.GroupName = item.ContactName;
                dashboardDto.ThisMonthContactWiseCustomerList.Add(customerCountDto);
            }
            return dashboardDto;
        }
    }
}
