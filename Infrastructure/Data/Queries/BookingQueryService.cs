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

        public List<BookingItemDto> GetBookingDetailsByDate(DateTime dateFrom, DateTime dateTo)
        {
            List<DateTime> dateList = new List<DateTime>();
            TimeSpan dateDiff = dateTo - dateFrom;
            for (int i = 0; i < dateDiff.Days + 1; i++)
            {
                dateList.Add(dateFrom.AddDays(i));
            }
            dateList = dateList.ToList();

            //cross join Team And Shift
            var teamShift = (from tm in _dbContext.Teams//.Where(a => a.Status == "Active")
                            from sft in _dbContext.ShiftInfos
                            select new
                            {
                                TeamId = tm.Id,
                                TeamName = tm.Name,
                                ShiftId = sft.Id,
                                ShiftName = sft.Name,
                                TeamStatus=tm.Status,                              
                                TeamLeaderName = tm.TeamLeaderName,
                            }).ToList();

            var teamShiftAndDate = (from tm in teamShift
                                   from dt in dateList
                                   select new
                                   {
                                       TeamId = tm.TeamId,
                                       TeamName = tm.TeamName,
                                       ShiftId = tm.ShiftId,
                                       ShiftName = tm.ShiftName,
                                       TeamLeaderName = tm.TeamLeaderName,
                                       TeamStatus=tm.TeamStatus,
                                       BookingDate = dt
                                   });

            var mobileNoWithIds = (from con in _dbContext.ContractDetails
                                   join fol in _dbContext.Followups on con.CustomerId equals fol.CustomerId
                                   join bk in _dbContext.Bookings.Where(a => a.BookingDate >= dateFrom.Date
                                        && a.BookingDate <= dateTo.Date).Where(a => a.Status != "Cancel")
                                        on fol.Id equals bk.FollowupId
                                   select con
                               )
                    .GroupBy(a => a.CustomerId)
                    .Select(r => new
                    {
                        CustomerId = r.Key,
                        MobileNo = string.Join(",", r.Select(a => a.MobileNo))
                    });

            //var mobileNoWithIds = _dbContext.ContractDetails
            //        .GroupBy(a => a.CustomerId)
            //        .Select(r => new
            //        {
            //            CustomerId = r.Key,
            //            MobileNo = string.Join(",", r.Select(a => a.MobileNo))
            //        });


            //retrive booking,followup,customer,area
            
            var bookingInfo = (from bk in _dbContext.Bookings.Where(a => a.BookingDate >= dateFrom.Date
                                        && a.BookingDate <= dateTo.Date).Where(a => a.Status != "Cancel")
                              join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                              join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                              join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                              join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                              join cty in _dbContext.Cities on ar.CityId equals cty.Id
                              join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                              join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                              join mnth in _dbContext.MonthList on fol.CustomerDoTheWorkingMonth equals mnth.Id
                              join numb in mobileNoWithIds on cus.Id equals numb.CustomerId
                              select new
                              {
                                  BookingId = bk.Id,
                                  TeamId = bk.TeamId,
                                  ShiftId = bk.ShiftId,
                                  EntryDate = bk.EntryDate.Date,
                                  BookingDate = bk.BookingDate.Date,
                                  FollowupId = bk.FollowupId,
                                  Status = bk.Status,
                                  AgreeAmount = fol.AgreeAmount,
                                  CustomerId = cus.Id,
                                  OrganizationName = cus.ClientName,
                                  Address = cus.Address,
                                  MobileNo=numb.MobileNo,
                                  CityName = cty.Name,
                                  AreaName = ar.Name,
                                  SubAreaName = sar.Name,
                                  ServiceName = srv.Name,
                                  EmployeeName = emp.Name,
                                  WorkingMonth = mnth.Name,
                                  FollowupCallDate = fol.FollowupCallDate.Date,
                                  Remarks = fol.Remarks,
                                  BookingNote=bk.BookingNote
                              });

            var resultFinal = (from tmSft in teamShiftAndDate
                              join bk in bookingInfo
                              on new { tmSft.ShiftId, tmSft.TeamId, tmSft.BookingDate.Date } equals new { bk.ShiftId, bk.TeamId, bk.BookingDate.Date }
                              into tmSftbk
                              from result in tmSftbk.DefaultIfEmpty()
                              orderby tmSft.BookingDate.Date
                              select new
                              {
                                  tmSft,
                                  result
                              });

            List<BookingItemDto> bookingDtos = new List<BookingItemDto>();
            foreach (var item in resultFinal)
            {
                BookingItemDto bookingItemDto = new BookingItemDto();
                bookingItemDto.TeamId = item.tmSft.TeamId;
                bookingItemDto.TeamName = item.tmSft.TeamName;
                bookingItemDto.TeamLeaderName = item.tmSft.TeamLeaderName;
                bookingItemDto.ShiftId = item.tmSft.ShiftId;
                bookingItemDto.ShiftName = item.tmSft.ShiftName;
                bookingItemDto.TrDate = item.tmSft.BookingDate.Date;
                bookingItemDto.EntryDate = item.result == null ? null : item.result.EntryDate.Date;
                bookingItemDto.BookingDate = item.result == null ? null : item.result.BookingDate.Date;
                bookingItemDto.BookingNote = item.result == null ? "" : item.result.BookingNote;
                bookingItemDto.FollowupId = item.result == null ? 0 : item.result.FollowupId;
                bookingItemDto.Status = item.result == null ? "-----" : "Booked";
                bookingItemDto.AgreeAmount = item.result == null ? 0 : item.result.AgreeAmount;
                bookingItemDto.CustomerId = item.result == null ? 0 : item.result.CustomerId;
                bookingItemDto.Name = item.result == null ? "" : item.result.OrganizationName;
                bookingItemDto.Address = item.result == null ? "" : item.result.Address;
                bookingItemDto.MobileNo = item.result == null ? "" : item.result.MobileNo;
                bookingItemDto.CityName = item.result == null ? "" : item.result.CityName;
                bookingItemDto.AreaName = item.result == null ? "" : item.result.AreaName;
                bookingItemDto.SubAreaName = item.result == null ? "" : item.result.SubAreaName;

                bookingItemDto.ServiceName = item.result == null ? "" : item.result.ServiceName;
                bookingItemDto.EmployeeName = item.result == null ? "" : item.result.EmployeeName;
                bookingItemDto.WorkingMonth = item.result == null ? "" : item.result.WorkingMonth;
                bookingItemDto.FollowupCallDate = item.result == null ? null : item.result.FollowupCallDate;
                bookingItemDto.Remarks = item.result == null ? null : item.result.Remarks;
                bookingItemDto.BookingId = item.result == null ? null : item.result.BookingId;
                if(item.tmSft.TeamStatus=="Active")
                {
                    bookingDtos.Add(bookingItemDto);
                }
                if (item.tmSft.TeamStatus == "Inactive" && bookingItemDto.BookingId != null)
                {
                    bookingDtos.Add(bookingItemDto);
                }

            }

            return bookingDtos;
        }

        public BookingDto GetCustomerAndBookingDetailsByBookingId(int bookingid)
        {
            var result = from cus in _dbContext.Customers
                         join fol in _dbContext.Followups on cus.Id equals fol.CustomerId
                         join bk in _dbContext.Bookings.Where(a => a.Id == bookingid) on fol.Id equals bk.FollowupId
                         join emp in _dbContext.Employees on fol.FollowupById equals emp.Id
                         join subar in _dbContext.SubAreas on cus.SubAreaId equals subar.Id
                         join ar in _dbContext.Areas on subar.AreaId equals ar.Id
                         join cty in _dbContext.Cities on ar.CityId equals cty.Id
                         join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                         join mnt in _dbContext.MonthList on fol.CustomerDoTheWorkingMonth equals mnt.Id
                         join cont in _dbContext.Contacts on cus.ContactId equals cont.Id
                         join cat in _dbContext.Categories on cus.CategoryId equals cat.Id
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
                             MarketingNextPlan = fol.MarketingNextPlan,
                             BookingById = bk.BookingById,
                             TeamId = bk.TeamId,
                             ShiftId = bk.ShiftId,
                             EntryDate = bk.EntryDate.Date,
                             BookingDate = bk.BookingDate.Date,
                             FollowupId = bk.FollowupId,
                             Status = bk.Status,
                             BookingId = bk.Id,
                             BookingNote=bk.BookingNote,
                             ContactName=cont.Name,
                             CategoryName=cat.Name,
                             ReferenceBy=cus.ReferenceBy
                         };

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
                bookingDto.FollowupId = data.FollowupId;
                bookingDto.BookingById = data.BookingById;
                bookingDto.TeamId = data.TeamId;
                bookingDto.ShiftId = data.ShiftId;
                bookingDto.EntryDate = data.EntryDate.Date;
                bookingDto.BookingDate = data.BookingDate.Date;
                bookingDto.Status = data.Status;
                bookingDto.Id = data.BookingId;
                bookingDto.BookingNote = data.BookingNote;
                bookingDto.ContactName = data.ContactName;
                bookingDto.CategoryName = data.CategoryName;
                bookingDto.ReferenceBy = data.ReferenceBy;
    }

    var buildingDetails = from bld in _dbContext.BuildingDetails.Where(a => a.CustomerId == bookingDto.CustomerId)
                                  join brnd in _dbContext.Brands
                                  on bld.BrandId equals brnd.Id
                                  select new
                                  {
                                      BrandName = brnd.Name,
                                      Quantity = bld.Quantity,
                                      Capacity = bld.Capacity
                                  };

            foreach (var item in buildingDetails)
            {
                BuildingDetailsDto building = new BuildingDetailsDto();
                building.BrandName = item.BrandName;
                building.Quantity = item.Quantity;
                building.Capacity = item.Capacity.ToString();
                bookingDto.BuildingDetails.Add(building);
            }

            var contractDetails = from con in _dbContext.ContractDetails.Where(a => a.CustomerId == bookingDto.CustomerId)
                                  join des in _dbContext.Designations
                                  on con.DesignationId equals des.Id
                                  select new
                                  {
                                      ClientName = con.Name,
                                      MobileNo = con.MobileNo,
                                      Designation = des.Name
                                  };

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

        public List<BookingItemDto> GetDueBookingDetailsByDate(DateTime dateFrom, DateTime dateTo)
        {

            //retrive booking,followup,customer,area

            var bookingInfo = (from bk in _dbContext.Bookings.Where(a => a.BookingDate >= dateFrom.Date
                                        && a.BookingDate <= dateTo.Date).Where(a => a.Status != "Cancel").Where(a => a.PaymentStatus == "Due")
                               join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                               join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                               join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                               join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                               join cty in _dbContext.Cities on ar.CityId equals cty.Id
                               join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                               join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                               join mnth in _dbContext.MonthList on fol.CustomerDoTheWorkingMonth equals mnth.Id
                               join tm in _dbContext.Teams on bk.TeamId equals tm.Id
                               join sft in _dbContext.ShiftInfos on bk.ShiftId equals sft.Id
                               select new
                               {
                                   BookingId = bk.Id,
                                   TeamId = bk.TeamId,
                                   ShiftId = bk.ShiftId,
                                   EntryDate = bk.EntryDate.Date,
                                   BookingDate = bk.BookingDate.Date,
                                   FollowupId = bk.FollowupId,
                                   Status = bk.Status,
                                   AgreeAmount = fol.AgreeAmount,
                                   CustomerId = cus.Id,
                                   OrganizationName = cus.ClientName,
                                   Address = cus.Address,
                                   MobileNo = "",//numb.MobileNo"",
                                   CityName = cty.Name,
                                   AreaName = ar.Name,
                                   SubAreaName = sar.Name,
                                   ServiceName = srv.Name,
                                   EmployeeName = emp.Name,
                                   WorkingMonth = mnth.Name,
                                   FollowupCallDate = fol.FollowupCallDate.Date,
                                   Remarks = fol.Remarks,
                                   BookingNote = bk.BookingNote,
                                   TeamName=tm.Name,
                                   TeamLeaderName=tm.TeamLeaderName,
                                   ShiftName=sft.Name,
                               });


            List<BookingItemDto> bookingDtos = new List<BookingItemDto>();
            foreach (var item in bookingInfo)
            {
                BookingItemDto bookingItemDto = new BookingItemDto();
                bookingItemDto.TeamId = item.TeamId;
                bookingItemDto.TeamName = item.TeamName;
                bookingItemDto.TeamLeaderName = item.TeamLeaderName;
                bookingItemDto.ShiftId = item.ShiftId;
                bookingItemDto.ShiftName = item.ShiftName;
                bookingItemDto.TrDate = item.BookingDate.Date;
                bookingItemDto.EntryDate = item.EntryDate;
                bookingItemDto.BookingDate = item.BookingDate.Date;
                bookingItemDto.BookingNote = item.BookingNote;
                bookingItemDto.FollowupId = item.FollowupId;
                bookingItemDto.Status = "Booked";
                bookingItemDto.AgreeAmount = item.AgreeAmount;
                bookingItemDto.CustomerId = item.CustomerId;
                bookingItemDto.Name = item.OrganizationName;
                bookingItemDto.Address = item.Address;
                bookingItemDto.MobileNo = item.MobileNo;
                bookingItemDto.CityName = item.CityName;
                bookingItemDto.AreaName = item.AreaName;
                bookingItemDto.SubAreaName = item.SubAreaName;

                bookingItemDto.ServiceName = item.ServiceName;
                bookingItemDto.EmployeeName = item.EmployeeName;
                bookingItemDto.WorkingMonth = item.WorkingMonth;
                bookingItemDto.FollowupCallDate = item.FollowupCallDate;
                bookingItemDto.Remarks = item.Remarks;
                bookingItemDto.BookingId = item.BookingId;
                bookingDtos.Add(bookingItemDto);

            }

            return bookingDtos;
        }

        public bool IsBookedAlready(int teamId, int shiftId, DateTime bookingDate)
        {
            return _dbContext.Bookings.Any(a => a.TeamId == teamId && a.ShiftId == shiftId && a.BookingDate == bookingDate && a.Status == "Booked");
        }
    }

}
