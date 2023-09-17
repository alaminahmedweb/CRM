using ApplicationCore.DtoModels;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public List<ServiceFeedbackDto> GetBookingList(DateTime dateFrom, DateTime dateTo)
        {
            var mobileNoWithIds = _dbContext.ContractDetails
                            .GroupBy(a => a.CustomerId)
                            .Select(r => new
                            {
                                CustomerId = r.Key,
                                MobileNo = string.Join(",", r.Select(a => a.MobileNo))
                            });

            var bookingInfo = from bk in _dbContext.Bookings.Where(a => a.BookingDate >= dateFrom.Date
                                    && a.BookingDate <= dateTo.Date).Where(a => a.Status != "Cancel")
                              join tm in _dbContext.Teams on bk.TeamId equals tm.Id
                              join sft in _dbContext.ShiftInfos on bk.ShiftId equals sft.Id
                              join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                              join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                              join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                              join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                              join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                              join cty in _dbContext.Cities on ar.CityId equals cty.Id
                              join numb in mobileNoWithIds on cus.Id equals numb.CustomerId
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
                                  ServiceName = srv.Name,
                                  FollowupId = bk.FollowupId,
                                  Status = bk.Status,
                                  AgreeAmount = fol.AgreeAmount,
                                  OrganizationName = cus.ClientName,
                                  Address = cus.Address,
                                  CityName = cty.Name,
                                  AreaName = ar.Name,
                                  SubAreaName = sar.Name,
                                  BookingNote = bk.BookingNote,
                                  MobileNo = numb.MobileNo
                              };

            var resultFinal = from bk in bookingInfo
                              join fd in _dbContext.ServiceFeedbacks
                              on bk.BookingId equals fd.BookingId
                              into bkfd
                              from result in bkfd.DefaultIfEmpty()
                              orderby bk.BookingDate
                              select new
                              {
                                  bk,
                                  result
                              };

            List<ServiceFeedbackDto> serviceFeedbackDtos = new List<ServiceFeedbackDto>();
            foreach (var item in resultFinal)
            {
                ServiceFeedbackDto serviceFeedbackDto = new ServiceFeedbackDto();
                serviceFeedbackDto.BookingId = item.bk.BookingId;
                serviceFeedbackDto.TeamName = item.bk.TeamName;
                serviceFeedbackDto.TeamLeaderName = item.bk.TeamLeaderName;
                serviceFeedbackDto.ShiftName = item.bk.ShiftName;
                serviceFeedbackDto.BookingDate = item.bk.BookingDate;
                serviceFeedbackDto.Amount = item.bk.AgreeAmount;
                serviceFeedbackDto.BookingNote = item.bk.BookingNote;
                serviceFeedbackDto.ServiceName = item.bk.ServiceName;
                serviceFeedbackDto.MobileNo = item.bk.MobileNo;
                serviceFeedbackDto.CustomerName = item.bk.OrganizationName;
                serviceFeedbackDto.Address = item.bk.Address;
                serviceFeedbackDto.CityName = item.bk.CityName;
                serviceFeedbackDto.AreaName = item.bk.AreaName;
                serviceFeedbackDto.SubAreaName = item.bk.SubAreaName;
                serviceFeedbackDto.EntryDateTime = item.result == null ? null : item.result.EntryDateTime;
                serviceFeedbackDto.CustomerFeedback = item.result == null ? "" : item.result.CustomerFeedback;
                serviceFeedbackDtos.Add(serviceFeedbackDto);
            }
            return serviceFeedbackDtos;
        }

        public ServiceFeedbackDto GetBookingListByid(int id)
        {
            var bookingInfo = from bk in _dbContext.Bookings.Where(a => a.Id == id)
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
                                  BookingNote = bk.BookingNote,
                                  OrganizationName = cus.ClientName,
                                  Address = cus.Address,
                                  CityName = cty.Name,
                                  AreaName = ar.Name,
                                  SubAreaName = sar.Name
                              };

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

            ServiceFeedbackDto serviceFeedbackDto = new ServiceFeedbackDto();
            foreach (var item in resultFinal)
            {
                serviceFeedbackDto.BookingId = item.bk.BookingId;
                serviceFeedbackDto.TeamName = item.bk.TeamName;
                serviceFeedbackDto.TeamLeaderName = item.bk.TeamLeaderName;
                serviceFeedbackDto.ShiftName = item.bk.ShiftName;
                serviceFeedbackDto.BookingDate = item.bk.BookingDate;
                serviceFeedbackDto.Amount = item.bk.AgreeAmount;
                serviceFeedbackDto.BookingNote = item.bk.BookingNote;
                serviceFeedbackDto.CustomerName = item.bk.OrganizationName;
                serviceFeedbackDto.Address = item.bk.Address;
                serviceFeedbackDto.CityName = item.bk.CityName;
                serviceFeedbackDto.AreaName = item.bk.AreaName;
                serviceFeedbackDto.SubAreaName = item.bk.SubAreaName;
                serviceFeedbackDto.EntryDateTime = item.result == null ? DateTime.Now : item.result.EntryDateTime;
                serviceFeedbackDto.CustomerFeedback = item.result == null ? "" : item.result.CustomerFeedback;
            }

            return serviceFeedbackDto;

        }

        public ComplainFeedbackDto GetComplainDetailsById(int id)
        {
            var complainInfo = from comp in _dbContext.Complains.Where(a => a.Id == id)
                               join bk in _dbContext.Bookings on comp.BookingId equals bk.Id
                               join tm in _dbContext.Teams on bk.TeamId equals tm.Id
                               join sft in _dbContext.ShiftInfos on bk.ShiftId equals sft.Id
                               join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                               join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                               join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                               join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                               join cty in _dbContext.Cities on ar.CityId equals cty.Id
                               select new
                               {
                                   CompainId = comp.Id,
                                   CompainDate = comp.ComplainDate,
                                   ComplainDetails = comp.ComplainDetails,
                                   TeamName = tm.Name,
                                   TeamLeaderName = tm.TeamLeaderName,
                                   ShiftName = sft.Name,
                                   BookingDate = bk.BookingDate,
                                   AgreeAmount = fol.AgreeAmount,
                                   BookingNote = bk.BookingNote,
                                   OrganizationName = cus.ClientName,
                                   Address = cus.Address,
                                   CityName = cty.Name,
                                   AreaName = ar.Name,
                                   SubAreaName = sar.Name
                               };

            ComplainFeedbackDto complainFeedbackDto = new ComplainFeedbackDto();
            foreach (var item in complainInfo)
            {
                complainFeedbackDto.ComplainId = item.CompainId;
                complainFeedbackDto.ComplainDetails = item.ComplainDetails;
                complainFeedbackDto.ComplainDate = item.CompainDate;
                complainFeedbackDto.TeamName = item.TeamName;
                complainFeedbackDto.TeamLeaderName = item.TeamLeaderName;
                complainFeedbackDto.ShiftName = item.ShiftName;
                complainFeedbackDto.BookingDate = item.BookingDate;
                complainFeedbackDto.Amount = item.AgreeAmount;
                complainFeedbackDto.BookingNote = item.BookingNote;
                complainFeedbackDto.CustomerName = item.OrganizationName;
                complainFeedbackDto.Address = item.Address;
                complainFeedbackDto.CityName = item.CityName;
                complainFeedbackDto.AreaName = item.AreaName;
                complainFeedbackDto.SubAreaName = item.SubAreaName;
            }

            return complainFeedbackDto;
        }

        public List<ComplainFeedbackDto> GetDailyComplainList(DateTime dateFrom, DateTime dateTo)
        {
            var mobileNoWithIds = _dbContext.ContractDetails
                            .GroupBy(a => a.CustomerId)
                            .Select(r => new
                            {
                                CustomerId = r.Key,
                                MobileNo = string.Join(",", r.Select(a => a.MobileNo))
                            });

            var complainInfo = from comp in _dbContext.Complains.Where(a => a.ComplainDate >= dateFrom.Date
                                        && a.ComplainDate <= dateTo.Date).Where(a => a.IsGivenFeedback == false)
                               join bk in _dbContext.Bookings on comp.BookingId equals bk.Id
                               join tm in _dbContext.Teams on bk.TeamId equals tm.Id
                               join sft in _dbContext.ShiftInfos on bk.ShiftId equals sft.Id
                               join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                               join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                               join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                               join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                               join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                               join numb in mobileNoWithIds on cus.Id equals numb.CustomerId
                               join cty in _dbContext.Cities on ar.CityId equals cty.Id
                               select new
                               {
                                   CompainId = comp.Id,
                                   CompainDate = comp.ComplainDate,
                                   ComplainDetails = comp.ComplainDetails,
                                   TeamName = tm.Name,
                                   TeamLeaderName = tm.TeamLeaderName,
                                   ShiftName = sft.Name,
                                   BookingDate = bk.BookingDate,
                                   ServiceName=srv.Name,
                                   AgreeAmount = fol.AgreeAmount,
                                   OrganizationName = cus.ClientName,
                                   Address = cus.Address,
                                   CityName = cty.Name,
                                   AreaName = ar.Name,
                                   SubAreaName = sar.Name,
                                   BookingNote = bk.BookingNote,
                                   MobileNo = numb.MobileNo
                               };

            var resultFinal = from comp in complainInfo
                              join fd in _dbContext.ComplainFeedback
                              on comp.CompainId equals fd.ComplainId
                              into compfd
                              from result in compfd.DefaultIfEmpty()
                              orderby comp.CompainId
                              select new
                              {
                                  comp,
                                  result
                              };

            List<ComplainFeedbackDto> complainFeedbackDtos = new List<ComplainFeedbackDto>();
            foreach (var item in resultFinal)
            {
                ComplainFeedbackDto complainFeedbackDto = new ComplainFeedbackDto();
                complainFeedbackDto.ComplainId = item.comp.CompainId;
                complainFeedbackDto.ComplainDetails = item.comp.ComplainDetails;
                complainFeedbackDto.ComplainDate = item.comp.CompainDate;
                complainFeedbackDto.TeamName = item.comp.TeamName;
                complainFeedbackDto.TeamLeaderName = item.comp.TeamLeaderName;
                complainFeedbackDto.ShiftName = item.comp.ShiftName;
                complainFeedbackDto.BookingDate = item.comp.BookingDate;
                complainFeedbackDto.Amount = item.comp.AgreeAmount;
                complainFeedbackDto.BookingNote = item.comp.BookingNote;
                complainFeedbackDto.ServiceName = item.comp.ServiceName;
                complainFeedbackDto.CustomerName = item.comp.OrganizationName;
                complainFeedbackDto.Address = item.comp.Address;
                complainFeedbackDto.CityName = item.comp.CityName;
                complainFeedbackDto.AreaName = item.comp.AreaName;
                complainFeedbackDto.SubAreaName = item.comp.SubAreaName;
                complainFeedbackDto.MobileNo = item.comp.MobileNo;
                complainFeedbackDto.ActionTakenAgainstComplain = item.result == null ? "" : item.result.ActionTakenAgainstComplain;
                complainFeedbackDto.ActionTakenDate = item.result == null ? null : item.result.ActionTakenDate;
                complainFeedbackDtos.Add(complainFeedbackDto);
            }

            return complainFeedbackDtos;
        }

        public bool IsAlreadyGivenFeedback(int bookingId)
        {
            return _dbContext.ServiceFeedbacks.Any(a => a.BookingId == bookingId);
        }
        public bool IsAlreadyGivenComplain(int bookingId)
        {
            return _dbContext.Complains.Any(a => a.BookingId == bookingId);
        }
        public bool IsAlreadyGivenComplainFeedback(int complainId)
        {
            return _dbContext.ComplainFeedback.Any(a => a.ComplainId == complainId);
        }
    }
}
