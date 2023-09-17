using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries
{
    public class FollowupQueryService : IFollowupQueryService
    {
        private readonly AppDbContext _dbContext;
        public FollowupQueryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomerFollowupDto GetCustomerDetailsByFollowupId(int followupId)
        {
            var result = from cus in _dbContext.Customers
                         join fol in _dbContext.Followups.Where(a => a.Id == followupId) on cus.Id equals fol.CustomerId
                         join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                         join subar in _dbContext.SubAreas on cus.SubAreaId equals subar.Id
                         join ar in _dbContext.Areas on subar.AreaId equals ar.Id
                         join cty in _dbContext.Cities on ar.CityId equals cty.Id
                         join ctg in _dbContext.Categories on cus.CategoryId equals ctg.Id
                         join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                         join con in _dbContext.Contacts on cus.ContactId equals con.Id
                         join mnth in _dbContext.MonthList on fol.CustomerDoTheWorkingMonth equals mnth.Id
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
                             ServiceTypeId = fol.ServiceTypeId,
                             OfferAmount = fol.OfferAmount,
                             AgreeAmount = fol.AgreeAmount,
                             CustomerDoTheWorkingMonth = mnth.Id,
                             CustomerDoTheWorkingMonthName = mnth.Name,
                             Remarks = fol.Remarks,
                             PositiveOrNegative = fol.PositiveOrNegative,
                             DiscussionDetailsNote = fol.DiscussionDetailsNote,
                             FollowupById = fol.FollowupById,
                             MarketingNextPlan = fol.MarketingNextPlan,
                             FollowupId = fol.Id,
                             Category = ctg.Name,
                             ReferenceBy = cus.ReferenceBy,
                             ContactName = con.Name
                         };

            CustomerFollowupDto customerFollowupDto = new CustomerFollowupDto();

            foreach (var data in result)
            {
                customerFollowupDto.CustomerId = data.CustomerId;
                customerFollowupDto.Name = data.CustomerName;
                customerFollowupDto.Address = data.Address;
                customerFollowupDto.AreaName = data.AreaName;
                customerFollowupDto.EmployeeName = data.EmployeeName;
                customerFollowupDto.CityName = data.CityName;
                customerFollowupDto.SubAreaName = data.SubAreaName;
                customerFollowupDto.NoOfFloor = data.NoOfFloor;
                customerFollowupDto.NoOfFlat = data.NoOfFlat;
                customerFollowupDto.ServiceTypeId = data.ServiceTypeId;
                customerFollowupDto.ServiceName = data.ServiceName;
                customerFollowupDto.OfferAmount = data.OfferAmount;
                customerFollowupDto.AgreeAmount = data.AgreeAmount;
                customerFollowupDto.CustomerDoTheWorkingMonth = data.CustomerDoTheWorkingMonth;
                customerFollowupDto.CustomerDoTheWorkingMonthName = data.CustomerDoTheWorkingMonthName;
                customerFollowupDto.Remarks = data.Remarks;
                customerFollowupDto.PositiveOrNegative = data.PositiveOrNegative;
                customerFollowupDto.DiscussionDetailsNote = data.DiscussionDetailsNote;
                customerFollowupDto.MarketingNextPlan = data.MarketingNextPlan;
                customerFollowupDto.ReferenceBy = data.ReferenceBy;
                customerFollowupDto.CategoryName = data.Category;
                customerFollowupDto.FollowupById = data.FollowupById;
                customerFollowupDto.BookingById = data.FollowupById;
                customerFollowupDto.FollowupId = data.FollowupId;
                customerFollowupDto.ContactName = data.ContactName;
            }
            var buildingDetails = from bld in _dbContext.BuildingDetails.Where(a => a.CustomerId == customerFollowupDto.CustomerId)
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
                building.Capacity = item.Capacity;
                customerFollowupDto.BuildingDetails.Add(building);
            }

            var contractDetails = from con in _dbContext.ContractDetails.Where(a => a.CustomerId == customerFollowupDto.CustomerId)
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
                customerFollowupDto.ContractDetails.Add(contractDetailsDto);
            }

            return customerFollowupDto;
        }
        public FollowupDetailsByIdDto GetFollowupDetailsByCustId(int customerId)
        {
            //Customer Info
            var customer = from cus in _dbContext.Customers.Where(a => a.Id == customerId)
                           join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                           join ctg in _dbContext.Categories on cus.CategoryId equals ctg.Id
                           join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                           join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                           join cty in _dbContext.Cities on ar.CityId equals cty.Id
                           join con in _dbContext.Contacts on cus.ContactId equals con.Id
                           select new
                           {
                               CustomerId = cus.Id,
                               CustomerName = cus.ClientName,
                               Address = cus.Address,
                               SubAreaName = sar.Name,
                               AreaName = ar.Name,
                               CityName = cty.Name,
                               EmployeeName = emp.Name,
                               ContactName = con.Name,
                               NoOfFloor = cus.NoOfFloor,
                               NoOfFlat = cus.NoOfFlat,
                               CategoryName = ctg.Name,
                               ReferenceBy = cus.ReferenceBy,
                           };

            FollowupDetailsByIdDto followupDetailsByIdDto = new FollowupDetailsByIdDto();

            foreach (var data in customer)
            {
                followupDetailsByIdDto.CustomerId = data.CustomerId;
                followupDetailsByIdDto.CustomerName = data.CustomerName;
                followupDetailsByIdDto.Address = data.Address;
                followupDetailsByIdDto.AreaName = data.AreaName;
                followupDetailsByIdDto.EmployeeName = data.EmployeeName;
                followupDetailsByIdDto.NoOfFlat = data.NoOfFlat;
                followupDetailsByIdDto.NoOfFloor = data.NoOfFloor;
                followupDetailsByIdDto.SubAreaName = data.SubAreaName;
                followupDetailsByIdDto.CityName = data.CityName;
                followupDetailsByIdDto.ContactName = data.ContactName;
                followupDetailsByIdDto.CategoryName = data.CategoryName;
                followupDetailsByIdDto.ReferenceBy = data.ReferenceBy;
            }

            //Followups
            var followups = from fol in _dbContext.Followups.Where(a => a.CustomerId == customerId)
                            join emp in _dbContext.Employees on fol.FollowupById equals emp.Id
                            join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                            join mnth in _dbContext.MonthList on fol.CustomerDoTheWorkingMonth equals mnth.Id
                            select new
                            {
                                FollowupDate = fol.FollowupCallDate.ToString("dd/MM/yyyy"),
                                CallingDate = fol.CallingDate,
                                OfferAmount = fol.OfferAmount,
                                AgreeAmount = fol.AgreeAmount,
                                CustomerDoTheWorkingMonth = mnth.Name,
                                Remarks = fol.Remarks,
                                PositiveOrNegative = fol.PositiveOrNegative,
                                DiscussionDetailsNote = fol.DiscussionDetailsNote,
                                MarketingNextPlan = fol.MarketingNextPlan,
                                FollowupCallDate = fol.FollowupCallDate,
                                Status = fol.Status,
                                ServiceName = srv.Name,
                                FollowupBy = emp.Name,
                                FollowupId = fol.Id
                            };


            foreach (var data in followups)
            {
                FollowupDto followupDto = new FollowupDto();
                followupDto.CallingDate = data.CallingDate.Date;
                followupDto.OfferAmount = data.OfferAmount;
                followupDto.AgreeAmount = data.AgreeAmount;
                followupDto.CustomerDoTheWorkingMonthName = data.CustomerDoTheWorkingMonth;
                followupDto.Remarks = data.Remarks;
                followupDto.PositiveOrNegative = data.PositiveOrNegative;
                followupDto.DiscussionDetailsNote = data.DiscussionDetailsNote;
                followupDto.FollowupCallDate = data.FollowupCallDate;
                followupDto.Status = data.Status;
                followupDto.MarketingNextPlan = data.MarketingNextPlan;
                followupDto.ServiceName = data.ServiceName;
                followupDto.FollowupBy = data.FollowupBy;
                followupDto.FollowupId = data.FollowupId;

                followupDetailsByIdDto.Followups.Add(followupDto);
            }

            var buildingDetails = from bld in _dbContext.BuildingDetails.Where(a => a.CustomerId == customerId)
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
                BuildingDetailsDto buildingDetailsDto = new BuildingDetailsDto();
                buildingDetailsDto.BrandName = item.BrandName;
                buildingDetailsDto.Quantity = item.Quantity;
                buildingDetailsDto.Capacity = item.Capacity;
                followupDetailsByIdDto.BuildingDetails.Add(buildingDetailsDto);
            }

            var contractDetails = from con in _dbContext.ContractDetails.Where(a => a.CustomerId == customerId)
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
                followupDetailsByIdDto.ContractDetails.Add(contractDetailsDto);
            }

            //Bookings
            var bookingInfo = from bk in _dbContext.Bookings
                              join fol in _dbContext.Followups
                                   on bk.FollowupId equals fol.Id
                              join cus in _dbContext.Customers.Where(cus => cus.Id == customerId) on fol.CustomerId equals cus.Id
                              join tm in _dbContext.Teams on bk.TeamId equals tm.Id
                              join sft in _dbContext.ShiftInfos on bk.ShiftId equals sft.Id
                              join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                              join emp in _dbContext.Employees on bk.BookingById equals emp.Id
                              join mnth in _dbContext.MonthList on fol.CustomerDoTheWorkingMonth equals mnth.Id
                              select new
                              {
                                  BookingId = bk.Id,
                                  TeamName = tm.Name,
                                  TeamLeaderName = tm.TeamLeaderName,
                                  ShiftName = sft.Name,
                                  EntryDate = bk.EntryDate,
                                  BookingDate = bk.BookingDate.Date,
                                  FollowupId = bk.FollowupId,
                                  Status = bk.Status,
                                  AgreeAmount = fol.AgreeAmount,
                                  OrganizationName = cus.ClientName,
                                  Address = cus.Address,
                                  ServiceName = srv.Name,
                                  EmployeeName = emp.Name,
                                  WorkingMonth = mnth.Name,
                                  FollowupCallDate = fol.FollowupCallDate.Date,
                                  Remarks = fol.Remarks,
                                  BookingNote=bk.BookingNote,
                              };

            var bookingAndFeedbacks = from bk in bookingInfo
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

            foreach (var item in bookingAndFeedbacks)
            {
                BookingItemDto bookingItemDto = new BookingItemDto();
                bookingItemDto.TeamName = item.bk.TeamName;
                bookingItemDto.TeamLeaderName = item.bk.TeamLeaderName;
                bookingItemDto.ShiftName = item.bk.ShiftName;
                bookingItemDto.TrDate = item.bk.BookingDate.Date;
                bookingItemDto.EntryDate = item.bk.EntryDate;
                bookingItemDto.BookingDate = item.bk.BookingDate.Date;
                bookingItemDto.BookingId = item.bk.BookingId;
                bookingItemDto.FollowupId = item.bk.FollowupId;
                bookingItemDto.Status = item.bk.Status;
                bookingItemDto.AgreeAmount = item.bk.AgreeAmount;
                bookingItemDto.Name = item.bk.OrganizationName;
                bookingItemDto.Address = item.bk.Address;
                bookingItemDto.BookingNote = item.bk.BookingNote;

                bookingItemDto.ServiceName = item.bk.ServiceName;
                bookingItemDto.EmployeeName = item.bk.EmployeeName;
                bookingItemDto.WorkingMonth = item.bk.WorkingMonth;
                bookingItemDto.FollowupCallDate = item.bk.FollowupCallDate.Date;
                bookingItemDto.Remarks = item.bk.Remarks;

                bookingItemDto.CustomerFeedback = item.result == null ? "" : item.result.CompanyFeedback;
                bookingItemDto.CompanyFeedback = item.result == null ? "" : item.result.CustomerFeedback;
                bookingItemDto.FeedbackEntryDate = item.result == null ? null : item.result.EntryDateTime;

                followupDetailsByIdDto.Bookings.Add(bookingItemDto);
            }

            var bookingSummary = from bk in _dbContext.Bookings.Where(a=>a.Status!="Cancel")
                                 join fol in _dbContext.Followups.Where(a => a.CustomerId == customerId) on bk.FollowupId equals fol.Id
                                 join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                                 group fol by new { bk.FollowupId, bk.BookingDate,srv.Name } into g
                                 select new
                                 {
                                     FollowupId = g.Key.FollowupId,
                                     BookingDate = g.Key.BookingDate,
                                     TotalBookingAmt = g.Sum(fol => fol.AgreeAmount),
                                     NoOfSlot = g.Count(),
                                     BookingAmtPerSlot = g.Average(fol => fol.AgreeAmount),
                                     ServiceName=g.Key.Name
                                 };

            foreach (var item in bookingSummary)
            {
                BookingSummaryDto bookingSummaryDto = new BookingSummaryDto();
                bookingSummaryDto.BookingDate = item.BookingDate;
                bookingSummaryDto.FollowupId = item.FollowupId;
                bookingSummaryDto.BookingAmtPerSlot = item.BookingAmtPerSlot;
                bookingSummaryDto.NoOfSlot = item.NoOfSlot;
                bookingSummaryDto.TotalBookingAmt = item.TotalBookingAmt;
                bookingSummaryDto.ServiceName = item.ServiceName;

                followupDetailsByIdDto.BookingSummary.Add(bookingSummaryDto);
            }

            //Compalins
            var complainInfo = from comp in _dbContext.Complains
                               join bk in _dbContext.Bookings on comp.BookingId equals bk.Id
                               join tm in _dbContext.Teams on bk.TeamId equals tm.Id
                               join sft in _dbContext.ShiftInfos on bk.ShiftId equals sft.Id
                               join fol in _dbContext.Followups on bk.FollowupId equals fol.Id
                               join cus in _dbContext.Customers.Where(cus => cus.Id == customerId) on fol.CustomerId equals cus.Id
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
                                   OrganizationName = cus.ClientName,
                                   Address = cus.Address,
                                   CityName = cty.Name,
                                   AreaName = ar.Name,
                                   SubAreaName = sar.Name
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
                complainFeedbackDto.CustomerName = item.comp.OrganizationName;
                complainFeedbackDto.Address = item.comp.Address;
                complainFeedbackDto.CityName = item.comp.CityName;
                complainFeedbackDto.AreaName = item.comp.AreaName;
                complainFeedbackDto.SubAreaName = item.comp.SubAreaName;
                complainFeedbackDto.ActionTakenAgainstComplain = item.result == null ? "" : item.result.ActionTakenAgainstComplain;
                complainFeedbackDto.ActionTakenDate = item.result == null ? null : item.result.ActionTakenDate;
                followupDetailsByIdDto.Complains.Add(complainFeedbackDto);
            }

            return followupDetailsByIdDto;
        }
        public async Task<bool> IsAlreadyGivenFollowup(int customerId, DateTime followupDate)
        {
            return await _dbContext.Followups.AnyAsync(a => a.CustomerId == customerId && a.FollowupCallDate == followupDate);
        }
        public async Task<Followup> GetFollowupByIdAsync(int id)
        {
            //return await DbSet.Find(id);
            return await _dbContext.Followups.FirstOrDefaultAsync(a => a.Id == id);
        }
        public int GetMaxFollowupIdByCustId(int customerId)
        {
            return _dbContext.Followups.Where(a => a.CustomerId == customerId).Max(a => a.Id);
        }
        public IEnumerable<CustomerDto> GetDailyFollowupListByMpoIdAndFollowupType(DateTime dateFrom, DateTime dateTo, int mpoId, string followupType)
        {
            IEnumerable<Customer> customerList;


            if (followupType == "BookedCustomer")
            {
                customerList = (from fol in _dbContext.Followups.Where(a => a.Status == "Confirmed")
                                join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                                select cus).Distinct().ToList();
            }
            else if (followupType == "NonBookedCustomer")
            {
                var NotBookedCustomer = (from e in _dbContext.Followups
                                         where e.Status != ""
                                         && !(from e2 in _dbContext.Followups
                                              where e2.Status == "Confirmed"
                                              select e2.CustomerId).ToList().Contains(e.CustomerId)
                                         select e);


                customerList = (from fol in NotBookedCustomer
                                join cus in _dbContext.Customers on fol.CustomerId equals cus.Id
                                select cus).Distinct().ToList();
            }
            else
            {
                customerList = (from cus in _dbContext.Customers
                                select cus).Distinct().ToList();
            }

            if (mpoId != -1)
            {
                customerList = customerList.Where(a => a.EmployeeId == mpoId).ToList();
            }

            //var mobileNoWithIds = _dbContext.ContractDetails
            //        .GroupBy(a => a.CustomerId)
            //        .Select(r => new
            //        {
            //            CustomerId = r.Key,
            //            MobileNo = string.Join(",", r.Select(a => a.MobileNo))
            //        });

            var result = from cus in customerList
                         join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                         join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                         join cty in _dbContext.Cities on ar.CityId equals cty.Id
                         join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                         join fol in _dbContext.Followups
                                   .Where(a => a.FollowupCallDate >= dateFrom.Date
                                   && a.FollowupCallDate <= dateTo
                                   && a.Status != "Inactive" && a.IsFollowupDone == false)
                         on cus.Id equals fol.CustomerId
                         //join numb in mobileNoWithIds on cus.Id equals numb.CustomerId
                         join cont in _dbContext.Contacts on cus.ContactId equals cont.Id
                         join cat in _dbContext.Categories on cus.CategoryId equals cat.Id
                         orderby fol.FollowupCallDate ascending
                         select new
                         {
                             CustomerId = cus.Id,
                             CustomerName = cus.ClientName,
                             Address = cus.Address,
                             CityName = cty.Name,
                             AreaName = ar.Name,
                             SubAreaName = sar.Name,
                             EmployeeName = emp.Name,
                             FollowupId = fol.Id,
                             FollowupDate = fol.FollowupCallDate.ToString("dd/MM/yyyy"),
                             CategoryName = cat.Name,
                             ContactName = cont.Name,
                             ReferenceBy = cus.ReferenceBy,
                             MobileNo = ""//numb.MobileNo
                         };

            List<CustomerDto> customerDtos = new List<CustomerDto>();
            foreach (var data in result)
            {
                CustomerDto customerDto = new CustomerDto();
                customerDto.CustomerId = data.CustomerId;
                customerDto.CustomerName = data.CustomerName;
                customerDto.Address = data.Address;
                customerDto.CityName = data.CityName;
                customerDto.AreaName = data.AreaName;
                customerDto.SubAreaName = data.SubAreaName;
                customerDto.EmployeeName = data.EmployeeName;
                customerDto.FollowupDate = data.FollowupDate;
                customerDto.FollowupId = data.FollowupId;

                customerDto.ContactName = data.ContactName;
                customerDto.CategoryName = data.CategoryName;
                customerDto.ReferenceBy = data.ReferenceBy;
                customerDto.MobileNo = data.MobileNo;
                customerDtos.Add(customerDto);
            }
            return customerDtos;
        }
    }
}
