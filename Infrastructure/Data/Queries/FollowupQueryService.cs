using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
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

        public CustomerDto GetCustomerDetailsByCustId(int id)
        {
            var result = (from cus in _dbContext.Customers.Where(a => a.Id == id)
                          join ar in _dbContext.Areas on cus.SubAreaId equals ar.Id
                          join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                          select new
                          {
                              CustomerId = cus.Id,
                              CustomerName = cus.ClientName,
                              ContractPerson = "",//cus.ContractPerson,
                              MobileNo = "",//cus.MobileNo,
                              Address = cus.Address,
                              BuildingDetails = "",//cus.BuildingDetails,
                              AreaName = ar.Name,
                              EmployeeName = emp.Name,
                          }).ToList();
            CustomerDto customerDto = new CustomerDto();
            foreach (var data in result)
            {
                customerDto.CustomerId = data.CustomerId;
                customerDto.CustomerName = data.CustomerName;
                customerDto.ContractPerson = data.ContractPerson;
                customerDto.MobileNo = data.MobileNo;
                customerDto.Address = data.Address;
                customerDto.BuildingDetails = data.BuildingDetails;
                customerDto.AreaName = data.AreaName;
                customerDto.EmployeeName = data.EmployeeName;
            }

            return customerDto;
        }

        public IEnumerable<CustomerDto> GetDailyFollowupList(DateTime date)
        {
            var result = (from cus in _dbContext.Customers
                          join ar in _dbContext.Areas on cus.SubAreaId equals ar.Id
                          join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                          join fol in _dbContext.Followups.Where(a=>a.FollowupCallDate==date.Date) 
                          on cus.Id equals fol.CustomerId 
                          select new
                          {
                              CustomerId = cus.Id,
                              CustomerName = cus.ClientName,
                              ContractPerson = "",//cus.ContractPerson,
                              MobileNo = "",//cus.MobileNo,
                              Address = cus.Address,
                              BuildingDetails ="",// cus.BuildingDetails,
                              AreaName = ar.Name,
                              EmployeeName = emp.Name,
                              FollowupDate = fol.FollowupCallDate.ToString("dd/MM/yyyy")
                          }).Distinct().ToList();
            List<CustomerDto> followupDtos = new List<CustomerDto>();
            foreach (var data in result)
            {
                CustomerDto follouwupdto = new CustomerDto();
                follouwupdto.CustomerId = data.CustomerId;
                follouwupdto.CustomerName = data.CustomerName;
                follouwupdto.ContractPerson = data.ContractPerson;
                follouwupdto.MobileNo = data.MobileNo;
                follouwupdto.Address = data.Address;
                follouwupdto.BuildingDetails = data.BuildingDetails;
                follouwupdto.AreaName = data.AreaName;
                follouwupdto.EmployeeName = data.EmployeeName;
                followupDtos.Add(follouwupdto);
            }

            return followupDtos;
        }
        public FollowupDetailsByIdDto GetFollowupDetailsByCustId(int id)
        {
            var result = (from cus in _dbContext.Customers
                          join ar in _dbContext.Areas on cus.SubAreaId equals ar.Id
                          join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                          join fol in _dbContext.Followups.Where(a => a.CustomerId == id) 
                          on cus.Id equals fol.CustomerId
                          select new
                          {
                              CustomerId = cus.Id,
                              CustomerName = cus.ClientName,
                              ContractPerson = "",//cus.ContractPerson,
                              MobileNo = "",//cus.MobileNo,
                              Address = cus.Address,
                              BuildingDetails = "",//cus.BuildingDetails,
                              AreaName = ar.Name,
                              EmployeeName = emp.Name,
                              FollowupDate = fol.FollowupCallDate.ToString("dd/MM/yyyy"),
                              CallingDate=fol.CallingDate,
                              OfferAmount=fol.OfferAmount,
                              AgreeAmount=fol.AgreeAmount,
                              CustomerDoTheWorkingMonth=fol.CustomerDoTheWorkingMonth,
                              Remarks=fol.Remarks,
                              PositiveOrNegative=fol.PositiveOrNegative,
                              DiscussionDetailsNote=fol.DiscussionDetailsNote,
                              MarketingNextPlan=fol.MarketingNextPlan,
                              FollowupCallDate=fol.FollowupCallDate,//.ToString("dd/MM/yyyy"),
                              Status=fol.Status
                          }).ToList();

            FollowupDetailsByIdDto follouwupdto = new FollowupDetailsByIdDto();
            foreach (var data in result)
            {
                follouwupdto.CustomerId = data.CustomerId;
                follouwupdto.CustomerName = data.CustomerName;
                follouwupdto.ContractPerson = data.ContractPerson;
                follouwupdto.MobileNo = data.MobileNo;
                follouwupdto.Address = data.Address;
                follouwupdto.BuildingDetails = data.BuildingDetails;
                follouwupdto.AreaName = data.AreaName;
                follouwupdto.EmployeeName = data.EmployeeName;

                FollowupDto followupDetailsDto = new FollowupDto();
                followupDetailsDto.CallingDate = data.CallingDate;
                followupDetailsDto.OfferAmount = data.OfferAmount;
                followupDetailsDto.AgreeAmount = data.AgreeAmount;
                followupDetailsDto.CustomerDoTheWorkingMonth = data.CustomerDoTheWorkingMonth.ToString();
                followupDetailsDto.Remarks = data.Remarks;
                followupDetailsDto.PositiveOrNegative = data.PositiveOrNegative;
                followupDetailsDto.DiscussionDetailsNote = data.DiscussionDetailsNote;
                followupDetailsDto.FollowupCallDate = data.FollowupCallDate;
                followupDetailsDto.Status = data.Status;

                follouwupdto.followups.Add(followupDetailsDto);
            }

            return follouwupdto;
        }
    }
}
