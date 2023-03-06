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

        public CustomerFollowupDto GetCustomerDetailsByCustId(int id)
        {
            int lastFollowupId = _dbContext.Followups.Where(a => a.CustomerId == id).Max(a => a.Id);

            var result = (from cus in _dbContext.Customers.Where(a => a.Id == id)
                          join fol in _dbContext.Followups.Where(a=>a.Id==lastFollowupId) on cus.Id equals fol.CustomerId
                          join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                          join subar in _dbContext.SubAreas on cus.SubAreaId equals subar.Id
                          join ar in _dbContext.Areas on subar.AreaId equals ar.Id
                          join cty in _dbContext.Cities on ar.CityId equals cty.Id
                          select new
                          {
                              CustomerId = cus.Id,
                              CustomerName = cus.ClientName,
                              Address = cus.Address,
                              AreaName = ar.Name,
                              EmployeeName = emp.Name,
                              CityName=cty.Name,
                              SubAreaName=subar.Name,
                              NoOfFloor=cus.NoOfFloor,
                              NoOfFlat=cus.NoOfFlat,
                              ServiceTypeId=fol.ServiceTypeId,
                              OfferAmount=fol.OfferAmount,
                              AgreeAmount=fol.AgreeAmount,
                              CustomerDoTheWorkingMonth=fol.CustomerDoTheWorkingMonth,
                              Remarks=fol.Remarks,
                              PositiveOrNegative=fol.PositiveOrNegative,
                              DiscussionDetailsNote=fol.DiscussionDetailsNote,
                              MarketingNextPlan=fol.MarketingNextPlan
                          }).ToList();
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
                customerFollowupDto.OfferAmount = data.OfferAmount;
                customerFollowupDto.AgreeAmount = data.AgreeAmount;
                customerFollowupDto.CustomerDoTheWorkingMonth = data.CustomerDoTheWorkingMonth;
                customerFollowupDto.Remarks = data.Remarks;
                customerFollowupDto.PositiveOrNegative = data.PositiveOrNegative;
                customerFollowupDto.DiscussionDetailsNote = data.DiscussionDetailsNote;
                customerFollowupDto.MarketingNextPlan = data.MarketingNextPlan;

            }
            var buildingDetails = (from bld in _dbContext.BuildingDetails.Where(a => a.CustomerId == id)
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
                customerFollowupDto.BuildingDetails.Add(building);
            }

            var contractDetails = (from con in _dbContext.ContractDetails.Where(a => a.CustomerId == id)
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
                customerFollowupDto.ContractDetails.Add(contractDetailsDto);
            }



            return customerFollowupDto;
        }

        public IEnumerable<CustomerDto> GetDailyFollowupList(DateTime date)
        {
            var result = (from cus in _dbContext.Customers
                          join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                          join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                          join cty in _dbContext.Cities on ar.CityId equals cty.Id
                          join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                          join fol in _dbContext.Followups.Where(a=>a.FollowupCallDate==date.Date) 
                          on cus.Id equals fol.CustomerId 
                          select new
                          {
                              CustomerId = cus.Id,
                              CustomerName = cus.ClientName,
                              Address = cus.Address,
                              CityName=cty.Name,
                              AreaName = ar.Name,
                              SubAreaName=sar.Name,
                              EmployeeName = emp.Name,
                              FollowupDate = fol.FollowupCallDate//.ToString("dd/MM/yyyy")
                          }).Distinct().ToList();

            List<CustomerDto> followupDtos = new List<CustomerDto>();
            foreach (var data in result)
            {
                CustomerDto follouwupdto = new CustomerDto();
                follouwupdto.CustomerId = data.CustomerId;
                follouwupdto.CustomerName = data.CustomerName;
                follouwupdto.Address = data.Address;
                follouwupdto.CityName = data.CityName;
                follouwupdto.AreaName = data.AreaName;
                follouwupdto.SubAreaName = data.SubAreaName;
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
                              Status=fol.Status,
                              NoOfFloor=cus.NoOfFloor,
                              NoOfFlat=cus.NoOfFlat
                          }).ToList();

            FollowupDetailsByIdDto follouwupdto = new FollowupDetailsByIdDto();
            foreach (var data in result)
            {
                follouwupdto.CustomerId = data.CustomerId;
                follouwupdto.CustomerName = data.CustomerName;
                follouwupdto.Address = data.Address;
                follouwupdto.AreaName = data.AreaName;
                follouwupdto.EmployeeName = data.EmployeeName;
                follouwupdto.NoOfFlat = data.NoOfFlat;
                follouwupdto.NoOfFloor = data.NoOfFloor;

                FollowupDto followupDetailsDto = new FollowupDto();
                followupDetailsDto.CallingDate = data.CallingDate;
                followupDetailsDto.OfferAmount = data.OfferAmount;
                followupDetailsDto.AgreeAmount = data.AgreeAmount;
                followupDetailsDto.CustomerDoTheWorkingMonthName = data.CustomerDoTheWorkingMonth.ToString();
                followupDetailsDto.Remarks = data.Remarks;
                followupDetailsDto.PositiveOrNegative = data.PositiveOrNegative;
                followupDetailsDto.DiscussionDetailsNote = data.DiscussionDetailsNote;
                followupDetailsDto.FollowupCallDate = data.FollowupCallDate;
                followupDetailsDto.Status = data.Status;

                follouwupdto.followups.Add(followupDetailsDto);
            }

            var buildingDetails = (from bld in _dbContext.BuildingDetails.Where(a => a.CustomerId == id)
                                   join brnd in _dbContext.Brands
                                   on bld.BrandId equals brnd.Id
                                   select new
                                   {
                                       BrandName = brnd.Name,
                                       Quantity = bld.Quantity,
                                       Capacity = bld.Capacity
                                   });                                  
            foreach(var item in buildingDetails)
            {
                BuildingDetailsDto building = new BuildingDetailsDto();
                building.BrandName= item.BrandName;
                building.Quantity= item.Quantity;
                building.Capacity= item.Capacity;
                follouwupdto.BuildingDetails.Add(building);
            }

            var contractDetails = (from con in _dbContext.ContractDetails.Where(a => a.CustomerId == id)
                                   join des in _dbContext.Designations
                                   on con.DesignationId equals des.Id
                                   select new
                                   {
                                       ClientName=con.Name,
                                       MobileNo=con.MobileNo,
                                       Designation=des.Name
                                   });
            foreach(var contract in contractDetails)
            {
                ContractDetailsDto contractDetailsDto = new ContractDetailsDto();
                contractDetailsDto.Name = contract.ClientName;
                contractDetailsDto.MobileNo = contract.MobileNo;
                contractDetailsDto.Designation=contract.Designation;
                follouwupdto.ContractDetails.Add(contractDetailsDto);
            }
            return follouwupdto;
        }
    }
}
