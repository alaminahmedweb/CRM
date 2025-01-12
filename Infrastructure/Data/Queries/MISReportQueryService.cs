using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries
{
    public class MISReportQueryService : IMISReportQueryService
    {
        private readonly AppDbContext _dbContext;

        public MISReportQueryService(AppDbContext appDbContext)
        {
            this._dbContext = appDbContext;
        }

        public IEnumerable<AllCustomerListDto> GetAllCustomersByDate(DateTime dateFrom, DateTime dateTo, int employeeId, int contactId,int categoryId)
        {
            var statusResult = _dbContext.Followups.Where(a => a.ModifiedDate.Date >= dateFrom && a.ModifiedDate.Date <= dateTo)
                            .Select(d => new
                            {
                                CustomerID = d.CustomerId,
                                Status = d.Status,
                                ModifiedDate = d.ModifiedDate.Date
                            })
                            .GroupBy(d => d.CustomerID)
                            .Select(g => g.OrderBy(d => d.ModifiedDate).FirstOrDefault()).ToList();

            var mobileNoWithIds = _dbContext.ContractDetails.Where(a => a.ModifiedDate.Date >= dateFrom && a.ModifiedDate.Date <= dateTo)
                                .GroupBy(a => a.CustomerId)
                                .Select(r => new
                                {
                                    CustomerId = r.Key,
                                    MobileNo = string.Join(",", r.Select(a => a.MobileNo))
                                }).ToList();

            var customerList = new List<Customer>();
            if (employeeId != -1)
            {
                customerList = _dbContext.Customers.Where(a => a.ModifiedDate.Date >= dateFrom && a.ModifiedDate.Date <= dateTo).Where(a => a.EmployeeId == employeeId).ToList();
            }
            else
            {
                customerList = _dbContext.Customers.Where(a => a.ModifiedDate.Date >= dateFrom && a.ModifiedDate.Date <= dateTo).ToList();
            }

            if (contactId != -1)
            {
                customerList = _dbContext.Customers.Where(a => a.ContactId == contactId).ToList();
            }

            if (categoryId!= -1)
            {
                customerList = _dbContext.Customers.Where(a => a.CategoryId== categoryId).ToList();
            }

            var result = (from cus in customerList
                          join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                          join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                          join cty in _dbContext.Cities on ar.CityId equals cty.Id
                          join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                          join cont in _dbContext.Contacts on cus.ContactId equals cont.Id
                          join cat in _dbContext.Categories on cus.CategoryId equals cat.Id
                          select new
                          {
                              CustomerId = cus.Id,
                              CustomerName = cus.ClientName,
                              Address = cus.Address,
                              CityName = cty.Name,
                              AreaName = ar.Name,
                              SubAreaName = sar.Name,
                              EmployeeName = emp.Name,
                              ContactName = cont.Name,
                              CategoryName = cat.Name,
                              ReferenceBy = cus.ReferenceBy
                          }).ToList();

            var finalresult = (from res in result
                               join mob in mobileNoWithIds on res.CustomerId equals mob.CustomerId
                               join stat in statusResult on res.CustomerId equals stat.CustomerID
                               select new
                               {
                                   CustomerId = res.CustomerId,
                                   CustomerName = res.CustomerName,
                                   Address = res.Address,
                                   CityName = res.CityName,
                                   AreaName = res.AreaName,
                                   SubAreaName = res.SubAreaName,
                                   EmployeeName = res.EmployeeName,
                                   ContactName = res.ContactName,
                                   CategoryName = res.CategoryName,
                                   ReferenceBy = res.ReferenceBy,
                                   MobileNo = mob.MobileNo,
                                   Status = stat.Status
                               });

            List<AllCustomerListDto> allCustomerList = new List<AllCustomerListDto>();

            foreach (var data in finalresult)
            {
                AllCustomerListDto customer = new AllCustomerListDto();
                customer.CustomerId = data.CustomerId;
                customer.CustomerName = data.CustomerName;
                customer.Address = data.Address;
                customer.AreaName = data.AreaName;
                customer.EmployeeName = data.EmployeeName;
                customer.CityName = data.CityName;
                customer.SubAreaName = data.SubAreaName;
                customer.ContactName = data.ContactName;
                customer.CategoryName = data.CategoryName;
                customer.ReferenceBy = data.ReferenceBy;
                customer.MobileNo = data.MobileNo;
                customer.Status = data.Status;
                allCustomerList.Add(customer);
            }
            return allCustomerList;
        }
        public IEnumerable<CustomerCountDto> GetContactWiseCustomersListByDate(DateTime dateFrom, DateTime dateTo)
        {

            var result = from cus in _dbContext.Customers.Where(a => a.ModifiedDate.Date >= dateFrom && a.ModifiedDate.Date <= dateTo)
                         join con in _dbContext.Contacts
                         on cus.ContactId equals con.Id
                         group con by new { con.Name } into g
                         select new
                         {
                             GroupName = g.Key.Name,
                             TotalCount = g.Count()
                         };


            List<CustomerCountDto> customerCountDtos = new List<CustomerCountDto>();

            foreach (var data in result)
            {
                CustomerCountDto customerCountDto = new CustomerCountDto();
                customerCountDto.GroupName = data.GroupName;
                customerCountDto.TotalCount = data.TotalCount;
                customerCountDtos.Add(customerCountDto);
            }
            return customerCountDtos;
        }

        public IEnumerable<CustomerDto> GetAllFollowupDoneListByDate(DateTime dateFrom, DateTime dateTo)
        {

            var mobileNoWithIds = (from con in _dbContext.ContractDetails
                                   join fol in _dbContext.Followups.Where(a => a.ModifiedDate.Date >= dateFrom.Date
                                   && a.ModifiedDate.Date <= dateTo)
                                   on con.CustomerId equals fol.CustomerId
                                   select con
                                   )
                    .GroupBy(a => a.CustomerId)
                    .Select(r => new
                    {
                        CustomerId = r.Key,
                        MobileNo = string.Join(",", r.Select(a => a.MobileNo))
                    });

            var result = from cus in _dbContext.Customers
                         join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                         join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                         join cty in _dbContext.Cities on ar.CityId equals cty.Id
                         join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                         join fol in _dbContext.Followups
                                   .Where(a => a.ModifiedDate.Date >= dateFrom.Date
                                   && a.ModifiedDate.Date <= dateTo)
                         on cus.Id equals fol.CustomerId
                         join numb in mobileNoWithIds on cus.Id equals numb.CustomerId
                         join cont in _dbContext.Contacts on cus.ContactId equals cont.Id
                         join cat in _dbContext.Categories on cus.CategoryId equals cat.Id
                         join srv in _dbContext.ServiceTypes on fol.ServiceTypeId equals srv.Id
                         where fol.ModifiedDate.Date != cus.ModifiedDate.Date
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
                             MobileNo = numb.MobileNo,
                             ServiceName = srv.Name,
                             Status = fol.Status
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
                customerDto.ServiceName = data.ServiceName;
                customerDto.Status = data.Status;
                customerDtos.Add(customerDto);
            }
            return customerDtos;
        }

        public List<BookingItemDto> GetEntryDateWiseBookingList(DateTime dateFrom, DateTime dateTo)
        {

            var mobileNoWithIds = (from con in _dbContext.ContractDetails
                                   join fol in _dbContext.Followups on con.CustomerId equals fol.CustomerId
                                   join bk in _dbContext.Bookings.Where(a => a.EntryDate >= dateFrom.Date
                                        && a.EntryDate <= dateTo.Date).Where(a => a.Status != "Cancel") 
                                        on fol.Id equals bk.FollowupId
                                   select con
                                           )
                                .GroupBy(a => a.CustomerId)
                                .Select(r => new
                                {
                                    CustomerId = r.Key,
                                    MobileNo = string.Join(",", r.Select(a => a.MobileNo))
                                });


            //retrive booking,followup,customer,area
            var bookingInfo = (from bk in _dbContext.Bookings.Where(a => a.EntryDate >= dateFrom.Date
                                        && a.EntryDate <= dateTo.Date).Where(a => a.Status != "Cancel")
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
                                   MobileNo = numb.MobileNo,
                                   CityName = cty.Name,
                                   AreaName = ar.Name,
                                   SubAreaName = sar.Name,
                                   ServiceName = srv.Name,
                                   EmployeeName = emp.Name,
                                   WorkingMonth = mnth.Name,
                                   FollowupCallDate = fol.FollowupCallDate.Date,
                                   Remarks = fol.Remarks,
                                   BookingNote = bk.BookingNote,
                                   TeamName = tm.Name,
                                   TeamLeaderName = tm.TeamLeaderName,
                                   ShiftName = sft.Name
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
                bookingItemDto.EntryDate = item.EntryDate.Date;
                bookingItemDto.BookingDate = item.BookingDate.Date;
                bookingItemDto.BookingNote = item.BookingNote;
                bookingItemDto.FollowupId = item.FollowupId;
                bookingItemDto.Status = item.Status;
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

                bookingItemDto.BookingEntryDate = item.EntryDate.ToString("dd/MM/yyyy");
                bookingItemDto.BookingWorkingDate = item.BookingDate.ToString("dd/MM/yyyy");
                bookingDtos.Add(bookingItemDto);
            }
            return bookingDtos;
        }

        public List<BookingItemDto> GetBookingCancelAndShiftList(DateTime dateFrom, DateTime dateTo, string status)
        {
            //var mobileNoWithIds = _dbContext.ContractDetails
            //        .GroupBy(a => a.CustomerId)
            //        .Select(r => new
            //        {
            //            CustomerId = r.Key,
            //            MobileNo = string.Join(",", r.Select(a => a.MobileNo))
            //        });

            var mobileNoWithIds = (from con in _dbContext.ContractDetails
                                   join fol in _dbContext.Followups on con.CustomerId equals fol.CustomerId
                                   join bk in _dbContext.Bookings.Where(a => a.ModifiedDate >= dateFrom.Date
                                        && a.ModifiedDate <= dateTo.Date).Where(a => a.Status == status)
                                        on fol.Id equals bk.FollowupId
                                   select con
                                           )
                                .GroupBy(a => a.CustomerId)
                                .Select(r => new
                                {
                                    CustomerId = r.Key,
                                    MobileNo = string.Join(",", r.Select(a => a.MobileNo))
                                });

            //retrive booking,followup,customer,area
            var bookingInfo = (from bk in _dbContext.Bookings.Where(a => a.ModifiedDate.Date >= dateFrom.Date
                                        && a.ModifiedDate.Date <= dateTo.Date).Where(a => a.Status == status)
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
                                   MobileNo = numb.MobileNo,
                                   CityName = cty.Name,
                                   AreaName = ar.Name,
                                   SubAreaName = sar.Name,
                                   ServiceName = srv.Name,
                                   EmployeeName = emp.Name,
                                   WorkingMonth = mnth.Name,
                                   FollowupCallDate = fol.FollowupCallDate.Date,
                                   Remarks = fol.Remarks,
                                   BookingNote = bk.BookingNote,
                                   TeamName = tm.Name,
                                   TeamLeaderName = tm.TeamLeaderName,
                                   ShiftName = sft.Name,
                                   ModifiedDate = bk.ModifiedDate
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
                bookingItemDto.EntryDate = item.EntryDate.Date;
                bookingItemDto.BookingDate = item.BookingDate.Date;
                bookingItemDto.BookingNote = item.BookingNote;
                bookingItemDto.FollowupId = item.FollowupId;
                bookingItemDto.Status = item.Status;
                bookingItemDto.AgreeAmount = item.AgreeAmount;
                bookingItemDto.CustomerId = item.CustomerId;
                bookingItemDto.Name = item.OrganizationName;
                bookingItemDto.Address = item.Address;
                bookingItemDto.MobileNo = item.MobileNo;
                bookingItemDto.CityName = item.CityName;
                bookingItemDto.AreaName = item.AreaName;
                bookingItemDto.SubAreaName = item.SubAreaName;
                bookingItemDto.ModifiedDate = item.ModifiedDate;

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

        public IEnumerable<CustomerCountDto> GetStatusWiseCustomersListByDate(DateTime dateFrom, DateTime dateTo)
        {
            var statusResult = _dbContext.Followups.Where(a => a.ModifiedDate.Date >= dateFrom && a.ModifiedDate.Date <= dateTo)
                            .Select(d => new
                            {
                                CustomerID = d.CustomerId,
                                Status = d.Status,
                                ModifiedDate = d.ModifiedDate.Date
                            })
                            .GroupBy(d => d.CustomerID)
                            .Select(g => g.OrderBy(d => d.ModifiedDate).FirstOrDefault()).ToList();

            var statusWiseResult = from stat in statusResult
                                   group stat by new { stat.Status } into g
                                   select new
                                   {
                                       GroupName = g.Key.Status,
                                       TotalCount = g.Count()
                                   };

            List<CustomerCountDto> customerCountDtos = new List<CustomerCountDto>();

            foreach (var data in statusWiseResult)
            {
                CustomerCountDto customerCountDto = new CustomerCountDto();
                customerCountDto.GroupName = data.GroupName;
                customerCountDto.TotalCount = data.TotalCount;
                customerCountDtos.Add(customerCountDto);
            }
            return customerCountDtos;

        }

        public IEnumerable<CustomerCountDto> GetStatusWiseFollowupDoneListByDate(DateTime dateFrom, DateTime dateTo)
        {
            var statusResult = from cus in _dbContext.Customers
                               join fol in _dbContext.Followups
                                         .Where(a => a.ModifiedDate.Date >= dateFrom.Date
                                         && a.ModifiedDate.Date <= dateTo)
                               on cus.Id equals fol.CustomerId
                               where fol.ModifiedDate.Date != cus.ModifiedDate.Date
                               orderby fol.FollowupCallDate ascending
                               select new
                               {
                                   Status = fol.Status
                               };

            var statusWiseResult = from stat in statusResult
                                   group stat by new { stat.Status } into g
                                   select new
                                   {
                                       GroupName = g.Key.Status,
                                       TotalCount = g.Count()
                                   };

            List<CustomerCountDto> customerCountDtos = new List<CustomerCountDto>();

            foreach (var data in statusWiseResult)
            {
                CustomerCountDto customerCountDto = new CustomerCountDto();
                customerCountDto.GroupName = data.GroupName;
                customerCountDto.TotalCount = data.TotalCount;
                customerCountDtos.Add(customerCountDto);
            }
            return customerCountDtos;

        }
    }
}
