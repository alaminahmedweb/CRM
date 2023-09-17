﻿using ApplicationCore.DtoModels;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Queries
{
    public class CustomerQueryService : ICustomerQueryService
    {
        private readonly AppDbContext _dbContext;

        public CustomerQueryService(AppDbContext appDbContext)
        {
            this._dbContext = appDbContext;
        }

        public bool IsMobileNoAlreadyExists(string mobileNo, int customerId)
        {
            bool isExists = _dbContext.ContractDetails.Any(a => a.MobileNo == mobileNo && a.CustomerId != customerId);
            return isExists;
        }

        public IEnumerable<AllCustomerListDto> GetAllCustomers()
        {
            var result = from cus in _dbContext.Customers
                         join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                         join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                         join cty in _dbContext.Cities on ar.CityId equals cty.Id
                         join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                         select new
                         {
                             CustomerId = cus.Id,
                             CustomerName = cus.ClientName,
                             Address = cus.Address,
                             CityName = cty.Name,
                             AreaName = ar.Name,
                             SubAreaName = sar.Name,
                             EmployeeName = emp.Name,
                         };

            List<AllCustomerListDto> allCustomerList = new List<AllCustomerListDto>();

            foreach (var data in result)
            {
                AllCustomerListDto customer = new AllCustomerListDto();
                customer.CustomerId = data.CustomerId;
                customer.CustomerName = data.CustomerName;
                customer.Address = data.Address;
                customer.AreaName = data.AreaName;
                customer.EmployeeName = data.EmployeeName;
                customer.CityName = data.CityName;
                customer.SubAreaName = data.SubAreaName;

                allCustomerList.Add(customer);

            }

            return allCustomerList;
        }

        public IEnumerable<AllCustomerListDto> GetAllCustomersByMobileNo(string term)
        {
            var result =
                from contr in _dbContext.ContractDetails
                .Where(a => a.MobileNo.Contains(term))
                join des in _dbContext.Designations on contr.DesignationId equals des.Id
                orderby contr.MobileNo descending
                select new
                {
                    CustomerId = contr.CustomerId,
                    CustomerName = contr.Name,
                    MobileNo = contr.MobileNo,
                    DesignationName = des.Name
                };

            List<AllCustomerListDto> allCustomerList = new List<AllCustomerListDto>();

            foreach (var data in result)
            {
                AllCustomerListDto customer = new AllCustomerListDto();
                customer.CustomerId = data.CustomerId;
                customer.CustomerName = data.CustomerName;
                customer.MobileNo = data.MobileNo;
                customer.DesignationName = data.DesignationName;

                allCustomerList.Add(customer);
            }

            return allCustomerList;
        }

        public CustomerDto GetCustomerById(int customerId)
        {
            var result = from cus in _dbContext.Customers.Where(a => a.Id == customerId)
                         join sar in _dbContext.SubAreas on cus.SubAreaId equals sar.Id
                         join ar in _dbContext.Areas on sar.AreaId equals ar.Id
                         join cty in _dbContext.Cities on ar.CityId equals cty.Id
                         join emp in _dbContext.Employees on cus.EmployeeId equals emp.Id
                         select new
                         {
                             CustomerId = cus.Id,
                             CustomerName = cus.ClientName,
                             Address = cus.Address,
                             CityId = cty.Id,
                             CityName = cty.Name,
                             AreaId = ar.Id,
                             AreaName = ar.Name,
                             SubAreaId = cus.SubAreaId,
                             SubAreaName = sar.Name,
                             EmployeeId = cus.EmployeeId,
                             EmployeeName = emp.Name,
                             CategoryId = cus.CategoryId,
                             ReferenceBy = cus.ReferenceBy,
                             ContactId = cus.ContactId,
                             ModifiedDate = cus.ModifiedDate,
                             NoOfFloor=cus.NoOfFloor,
                             NoOfFlat=cus.NoOfFlat

                         };

            CustomerDto customer = new CustomerDto();

            foreach (var data in result)
            {
                customer.CustomerId = data.CustomerId;
                customer.ClientName = data.CustomerName;
                customer.NoOfFloor = data.NoOfFloor;
                customer.NoOfFlat = data.NoOfFlat;
                customer.Address = data.Address;
                customer.AreaId = data.AreaId;
                customer.AreaName = data.AreaName;
                customer.EmployeeId = data.EmployeeId;
                customer.EmployeeName = data.EmployeeName;
                customer.CityId = data.CityId;
                customer.CityName = data.CityName;
                customer.SubAreaId = data.SubAreaId;
                customer.SubAreaName = data.SubAreaName;
                customer.CategoryId = data.CategoryId;
                customer.ReferenceBy = data.ReferenceBy;
                customer.ContactId = data.ContactId;
                customer.ModifiedDate = data.ModifiedDate;
            }

            var buildingDetails = from bld in _dbContext.BuildingDetails.Where(a => a.CustomerId == customerId)
                                  join brnd in _dbContext.Brands
                                  on bld.BrandId equals brnd.Id
                                  select new
                                  {
                                      Id = bld.Id,
                                      BrandId = bld.BrandId,
                                      BrandName = brnd.Name,
                                      Quantity = bld.Quantity,
                                      Capacity = bld.Capacity,
                                      CustomerId = bld.CustomerId
                                  };

            foreach (var item in buildingDetails)
            {
                BuildingDetailsDto building = new BuildingDetailsDto();
                building.Id = item.Id;
                building.BrandId = item.BrandId;
                building.BrandName = item.BrandName;
                building.Quantity = item.Quantity;
                building.Capacity = item.Capacity.ToString();
                building.CustomerId = item.CustomerId;
                customer.BuildingDetails.Add(building);
            }

            var contractDetails = from con in _dbContext.ContractDetails.Where(a => a.CustomerId == customerId)
                                  join des in _dbContext.Designations
                                  on con.DesignationId equals des.Id
                                  select new
                                  {
                                      Id = con.Id,
                                      ClientName = con.Name,
                                      MobileNo = con.MobileNo,
                                      Designation = des.Name,
                                      DesignationId = con.DesignationId
                                  };

            foreach (var contract in contractDetails)
            {
                ContractDetailsDto contractDetailsDto = new ContractDetailsDto();
                contractDetailsDto.Id = contract.Id;
                contractDetailsDto.Name = contract.ClientName;
                contractDetailsDto.MobileNo = contract.MobileNo;
                contractDetailsDto.DesignationId = contract.DesignationId;
                contractDetailsDto.Designation = contract.Designation;
                customer.ContractDetails.Add(contractDetailsDto);
            }
            return customer;
        }
    }
}
