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
                                    TeamLeaderName=tm.TeamLeaderName,
                                    TeamAndShift=tm.Id+sft.Id
                                }).ToList();

            //retrive booking,followup,customer,area
            var bookingInfo = (from bk in _dbContext.Bookings.Where(a => a.BookingDate == date.Date)
                               join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                               join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                               join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                               join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                               join cty in _dbContext.Cities on ar.CityId equals cty.Id
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
                                   Address = cus.Address,
                                   CityName=cty.Name,
                                   AreaName = ar.Name,
                                   SubAreaName=sar.Name

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
                bookingItemDto.TeamLeaderName = item.tmSft.TeamLeaderName;
                bookingItemDto.ShiftId = item.tmSft.ShiftId;
                bookingItemDto.ShiftName = item.tmSft.ShiftName;
                bookingItemDto.EntryDate = item.result == null? "": item.result.EntryDate.ToString();
                bookingItemDto.BookingDate = item.result == null ? "" : item.result.BookingDate.ToString();
                bookingItemDto.FollowupId = item.result==null ? 0: item.result.FollowupId;
                bookingItemDto.Status = item.result==null? "-----":"Booked";
                bookingItemDto.AgreeAmount = item.result == null ?"": item.result.AgreeAmount.ToString();
                bookingItemDto.Name = item.result==null?"": item.result.OrganizationName;
                bookingItemDto.Address = item.result==null?"": item.result.Address;
                bookingItemDto.CityName = item.result == null ? "" : item.result.CityName;
                bookingItemDto.AreaName = item.result==null? "":item.result.AreaName;
                bookingItemDto.SubAreaName = item.result == null ? "" : item.result.SubAreaName;
                bookingDtos.Add(bookingItemDto);
            }

            return bookingDtos;
        }


        public BookingDto GetFollowupDetailsById(int id)
        {
            
            var result = (from cus in _dbContext.Customers
                          join fol in _dbContext.Followups.Where(a => a.Id == id) on cus.Id equals fol.CustomerId
                          join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                          join subar in _dbContext.SubAreas on cus.SubAreaId equals subar.Id
                          join ar in _dbContext.Areas on subar.AreaId equals ar.Id
                          join cty in _dbContext.Cities on ar.CityId equals cty.Id
                          join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                          join mnt in _dbContext.MonthList on fol.CustomerDoTheWorkingMonth equals mnt.Id
                          select new
                          {
                              CustomerId = cus.Id,
                              CustomerName = cus.ClientName,
                              Address = cus.Address,
                              AreaName = ar.Name,
                              EmployeeName = emp.Name,
                              CityName = cty.Name,
                              SubAreaName = subar.Name,
                              NoOfFloor = cus.NoOfFloor,
                              NoOfFlat = cus.NoOfFlat,
                              ServiceName = srv.Name,
                              OfferAmount = fol.OfferAmount,
                              AgreeAmount = fol.AgreeAmount,
                              CustomerDoTheWorkingMonth = mnt.Name,
                              Remarks = fol.Remarks,
                              PositiveOrNegative = fol.PositiveOrNegative,
                              DiscussionDetailsNote = fol.DiscussionDetailsNote,
                              MarketingNextPlan = fol.MarketingNextPlan
                          }).ToList();
            BookingDto bookingDto = new BookingDto();

            foreach (var data in result)
            {
                bookingDto.CustomerId = data.CustomerId;
                bookingDto.Name = data.CustomerName;
                bookingDto.Address = data.Address;
                bookingDto.AreaName = data.AreaName;
                bookingDto.EmployeeName = data.EmployeeName;
                bookingDto.CityName = data.CityName;
                bookingDto.SubAreaName = data.SubAreaName;
                bookingDto.NoOfFloor = data.NoOfFloor;
                bookingDto.NoOfFlat = data.NoOfFlat;
                bookingDto.ServiceName = data.ServiceName;
                bookingDto.OfferAmount = data.OfferAmount;
                bookingDto.AgreeAmount = data.AgreeAmount;
                bookingDto.CustomerDoTheWorkingMonth = data.CustomerDoTheWorkingMonth;
                bookingDto.Remarks = data.Remarks;
                bookingDto.PositiveOrNegative = data.PositiveOrNegative;
                bookingDto.DiscussionDetailsNote = data.DiscussionDetailsNote;
                bookingDto.MarketingNextPlan = data.MarketingNextPlan;
                bookingDto.FollowupId = id;
            }
            var buildingDetails = (from bld in _dbContext.BuildingDetails.Where(a => a.CustomerId == bookingDto.CustomerId)
                                   join brnd in _dbContext.Brands
                                   on bld.BrandId equals brnd.Id
                                   select new
                                   {
                                       BrandName = brnd.Name,
                                       Quantity = bld.Quantity,
                                       Capacity = bld.Capacity
                                   });
            foreach (var item in buildingDetails)
            {
                BuildingDetailsDto building = new BuildingDetailsDto();
                building.BrandName = item.BrandName;
                building.Quantity = item.Quantity;
                building.Capacity = item.Capacity;
                bookingDto.BuildingDetails.Add(building);
            }

            var contractDetails = (from con in _dbContext.ContractDetails.Where(a => a.CustomerId == bookingDto.CustomerId)
                                   join des in _dbContext.Designations
                                   on con.DesignationId equals des.Id
                                   select new
                                   {
                                       ClientName = con.Name,
                                       MobileNo = con.MobileNo,
                                       Designation = des.Name
                                   });
            foreach (var contract in contractDetails)
            {
                ContractDetailsDto contractDetailsDto = new ContractDetailsDto();
                contractDetailsDto.Name = contract.ClientName;
                contractDetailsDto.MobileNo = contract.MobileNo;
                contractDetailsDto.Designation = contract.Designation;
                bookingDto.ContractDetails.Add(contractDetailsDto);
            }

            return bookingDto;
        }


    }
}
