using ApplicationCore.DtoModels;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries
{
    public class FeedbackQueryService : IFeedbackQueryService
    {
        private readonly AppDbContext _dbContext;

        public FeedbackQueryService(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public List<ServiceFeedbackDto> GetBookingList(DateTime date)
        {
            var bookingInfo = (from bk in _dbContext.Bookings.Where(a => a.BookingDate == date.Date)
                               join tm in _dbContext.Teams on bk.TeamId equals tm.Id
                               join sft in _dbContext.ShiftInfos on bk.ShiftId equals sft.Id
                               join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                               join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                               join ar in _dbContext.Areas on cus.SubAreaId equals ar.Id
                               select new
                               {
                                   BookingId = bk.Id,
                                   TeamId = bk.TeamId,
                                   TeamName=tm.Name,
                                   ShiftId = bk.ShiftId,
                                   ShiftName=sft.Name,
                                   TeamAndShift = bk.TeamId + bk.ShiftId,
                                   EntryDate = bk.EntryDate,
                                   BookingDate = bk.BookingDate,
                                   FollowupId = bk.FollowupId,
                                   Status = bk.Status,
                                   AgreeAmount = fol.AgreeAmount,
                                   OrganizationName = cus.ClientName,
                                   ContractPerson = "",//cus.ContractPerson,
                                   MobileNo = "",//cus.MobileNo,
                                   Address = cus.Address,
                                   BuildingDetails = "",//cus.BuildingDetails,
                                   AreaName = ar.Name
                               }).ToList();

            //var feedbackDetails= (from feed in _dbContext.ServiceFeedbacks
            //                      select new
            //                      {
            //                          FeedbackId = feed.Id,
            //                          FeedbackDetails = feed.FeedbackDetails,
            //                          EntryDate = feed.EntryDateTime,
            //                          BookingId = feed.bookingId
            //                      }).ToList();

            var resultFinal = from bk in bookingInfo
                         join fd in _dbContext.ServiceFeedbacks
                         on bk.BookingId equals fd.bookingId
                         into bkfd
                         from result in bkfd.DefaultIfEmpty()
                         orderby bk.TeamId
                         select new
                         {
                             bk,
                             result
                         };
            List<ServiceFeedbackDto> feedbackDtos = new List<ServiceFeedbackDto>();
            foreach (var item in resultFinal)
            {
                ServiceFeedbackDto feedbackDto = new ServiceFeedbackDto();
                feedbackDto.BookingId = item.bk.BookingId;
                feedbackDto.TeamName = item.bk.TeamName;
                feedbackDto.ShiftName = item.bk.ShiftName;
                feedbackDto.BookingDate = item.bk.BookingDate;
                feedbackDto.Amount =item.bk.AgreeAmount;
                feedbackDto.CustomerName = item.bk.OrganizationName;
                feedbackDto.ContractPerson = item.bk.ContractPerson;
                feedbackDto.MobileNo = item.bk.MobileNo;
                feedbackDto.Address = item.bk.Address;
                feedbackDto.BuildingDetails =  item.bk.BuildingDetails;
                feedbackDto.AreaName = item.result == null ? "" : item.bk.AreaName;
                feedbackDto.FeedbackEntryDate = item.result == null ? null : item.result.EntryDateTime;
                feedbackDto.FeedbackDetails = item.result == null ? "" : item.result.FeedbackDetails;
                feedbackDtos.Add(feedbackDto);
            }

            return feedbackDtos;

        }
    }
}
