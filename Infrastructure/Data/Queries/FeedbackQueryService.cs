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
                               join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                               join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                               join cty in _dbContext.Cities on ar.CityId equals cty.Id
                               select new
                               {
                                   BookingId = bk.Id,
                                   TeamId = bk.TeamId,
                                   TeamName=tm.Name,
                                   TeamLeaderName=tm.TeamLeaderName,
                                   ShiftId = bk.ShiftId,
                                   ShiftName=sft.Name,
                                   TeamAndShift = bk.TeamId + bk.ShiftId,
                                   EntryDate = bk.EntryDate,
                                   BookingDate = bk.BookingDate,
                                   FollowupId = bk.FollowupId,
                                   Status = bk.Status,
                                   AgreeAmount = fol.AgreeAmount,
                                   OrganizationName = cus.ClientName,
                                   Address = cus.Address,
                                   CityName=cty.Name,
                                   AreaName = ar.Name,
                                   SubAreaName=sar.Name
                               }).ToList();

            var resultFinal = from bk in bookingInfo
                         join fd in _dbContext.ServiceFeedbacks
                         on bk.BookingId equals fd.BookingId
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
                feedbackDto.TeamLeaderName = item.bk.TeamLeaderName;
                feedbackDto.ShiftName = item.bk.ShiftName;
                feedbackDto.BookingDate = item.bk.BookingDate;
                feedbackDto.Amount =item.bk.AgreeAmount;
                feedbackDto.CustomerName = item.bk.OrganizationName;
                feedbackDto.Address = item.bk.Address;
                feedbackDto.CityName = item.bk.CityName;
                feedbackDto.AreaName = item.bk.AreaName;
                feedbackDto.SubAreaName = item.bk.SubAreaName;
                feedbackDto.EntryDateTime = item.result == null ? null : item.result.EntryDateTime;
                feedbackDto.CustomerFeedback = item.result == null ? "" : item.result.CustomerFeedback;
                feedbackDtos.Add(feedbackDto);
            }

            return feedbackDtos;

        }

        public ServiceFeedbackDto GetBookingListByid(int id)
        {
            var bookingInfo = (from bk in _dbContext.Bookings.Where(a => a.Id == id)
                               join tm in _dbContext.Teams on bk.TeamId equals tm.Id
                               join sft in _dbContext.ShiftInfos on bk.ShiftId equals sft.Id
                               join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                               join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                               join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                               join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                               join cty in _dbContext.Cities on ar.CityId equals cty.Id
                               select new
                               {
                                   BookingId = bk.Id,
                                   TeamId = bk.TeamId,
                                   TeamName = tm.Name,
                                   TeamLeaderName = tm.TeamLeaderName,
                                   ShiftId = bk.ShiftId,
                                   ShiftName = sft.Name,
                                   TeamAndShift = bk.TeamId + bk.ShiftId,
                                   EntryDate = bk.EntryDate,
                                   BookingDate = bk.BookingDate,
                                   FollowupId = bk.FollowupId,
                                   Status = bk.Status,
                                   AgreeAmount = fol.AgreeAmount,
                                   OrganizationName = cus.ClientName,
                                   Address = cus.Address,
                                   CityName = cty.Name,
                                   AreaName = ar.Name,
                                   SubAreaName = sar.Name
                               }).ToList();

            var resultFinal = from bk in bookingInfo
                              join fd in _dbContext.ServiceFeedbacks
                              on bk.BookingId equals fd.BookingId
                              into bkfd
                              from result in bkfd.DefaultIfEmpty()
                              orderby bk.TeamId
                              select new
                              {
                                  bk,
                                  result
                              };
            ServiceFeedbackDto feedbackDto = new ServiceFeedbackDto();
            foreach (var item in resultFinal)
            {
                feedbackDto.BookingId = item.bk.BookingId;
                feedbackDto.TeamName = item.bk.TeamName;
                feedbackDto.TeamLeaderName = item.bk.TeamLeaderName;
                feedbackDto.ShiftName = item.bk.ShiftName;
                feedbackDto.BookingDate = item.bk.BookingDate;
                feedbackDto.Amount = item.bk.AgreeAmount;
                feedbackDto.CustomerName = item.bk.OrganizationName;
                feedbackDto.Address = item.bk.Address;
                feedbackDto.CityName = item.bk.CityName;
                feedbackDto.AreaName = item.bk.AreaName;
                feedbackDto.SubAreaName = item.bk.SubAreaName;
                feedbackDto.EntryDateTime = item.result == null ? null : item.result.EntryDateTime;
                feedbackDto.CustomerFeedback = item.result == null ? "" : item.result.CustomerFeedback;
            }

            return feedbackDto;

        }

    }
}
