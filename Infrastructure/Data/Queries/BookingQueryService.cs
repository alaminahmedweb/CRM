using ApplicationCore.DtoModels;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries
{
    public class BookingQueryService : IBookingQueryService
    {
        private readonly AppDbContext _dbContext;

        public BookingQueryService(AppDbContext appDbContext)
        {
            this._dbContext = appDbContext;
        }
        public List<BookingItemDto> GetBookingDetailsByDate(DateTime date)
        {
            //cross join Team And Shift
            var teamAndShift = (from tm in _dbContext.Teams.Where(a => a.Status == "Active")
                                from sft in _dbContext.ShiftInfos
                                select new
                                {
                                    TeamId = tm.Id,
                                    TeamName = tm.Name,
                                    ShiftId = sft.Id,
                                    ShiftName = sft.Name,
                                    TeamAndShift=tm.Id+sft.Id
                                }).ToList();

            //retrive booking,followup,customer,area
            var bookingInfo = (from bk in _dbContext.Bookings.Where(a => a.BookingDate == date.Date)
                               join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                               join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                               join ar in _dbContext.Areas on cus.SubAreaId equals ar.Id
                               select new
                               {
                                   BookingId = bk.Id,
                                   TeamId = bk.TeamId,
                                   ShiftId = bk.ShiftId,
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

            //left join between teamAndShift and bookings
            var resultFinal = from tmSft in teamAndShift
                               join bk in bookingInfo
                               on tmSft.TeamAndShift equals bk.TeamAndShift
                               into tmSftbk
                               from result in tmSftbk.DefaultIfEmpty()
                               orderby tmSft.TeamId
                               select new
                               {
                                   tmSft,
                                   result
                               };


            List<BookingItemDto> bookingDtos = new List<BookingItemDto>();
            foreach(var item in resultFinal)
            {
                BookingItemDto bookingItemDto = new BookingItemDto();
                bookingItemDto.TeamId = item.tmSft.TeamId;
                bookingItemDto.TeamName = item.tmSft.TeamName;
                bookingItemDto.ShiftId = item.tmSft.ShiftId;
                bookingItemDto.ShiftName = item.tmSft.ShiftName;
                bookingItemDto.EntryDate = item.result == null? null: item.result.EntryDate;
                bookingItemDto.BookingDate = item.result == null ? null : item.result.BookingDate;
                bookingItemDto.FollowupId = item.result==null ? 0: item.result.FollowupId;
                bookingItemDto.Status = item.result==null? "-----":"Booked";
                bookingItemDto.AgreeAmount = item.result == null ?null: item.result.AgreeAmount;
                bookingItemDto.Name = item.result==null?"": item.result.OrganizationName;
                bookingItemDto.ContractPerson = item.result==null? "": item.result.ContractPerson;
                bookingItemDto.MobileNo = item.result==null?"":item.result.MobileNo;
                bookingItemDto.Address = item.result==null?"": item.result.Address;
                bookingItemDto.BuildingDetails = item.result==null ? "": item.result.BuildingDetails;
                bookingItemDto.AreaName = item.result==null? "":item.result.AreaName;
                bookingDtos.Add(bookingItemDto);
            }

            return bookingDtos;
        }

        public BookingDto GetFollowupDetailsById(int id)
        {
            var followupDetails = (from fol in _dbContext.Followups.Where(a => a.Id == id)
                                   join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                                   join ar in _dbContext.Areas on cus.SubAreaId equals ar.Id
                                   select new
                                   {
                                       CustomerId = fol.CustomerId,
                                       CustomerName = cus.ClientName,
                                       ContractPerson = "",//cus.ContractPerson,
                                       MobileNo ="" ,//cus.MobileNo,
                                       Address = cus.Address,
                                       BuildingDetails = "",//cus.BuildingDetails,
                                       AreaName = ar.Name,
                                       AgreeAmount=fol.AgreeAmount,
                                       FollowupId=fol.Id
                                   }).ToList();
            BookingDto followupDetailsDto = new BookingDto();
            foreach(var item in followupDetails)
            {
                followupDetailsDto.Name = item.CustomerName;
                followupDetailsDto.ContractPerson = item.ContractPerson;
                followupDetailsDto.MobileNo = item.MobileNo;
                followupDetailsDto.Address = item.Address;  
                followupDetailsDto.BuildingDetails = item.BuildingDetails;  
                followupDetailsDto.AreaName = item.AreaName;
                followupDetailsDto.AgreeAmount = item.AgreeAmount;
                followupDetailsDto.FollowupId = item.FollowupId;
            }
            return followupDetailsDto;
        }
    }
}
