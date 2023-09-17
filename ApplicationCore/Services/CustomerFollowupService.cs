using ApplicationCore.DtoModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class CustomerFollowupService : ICustomerFollowupService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<BuildingDetails> _buildingDetailsRepository;
        private readonly IRepository<ContractDetails> _contractDetailsRepository;
        private readonly IRepository<Followup> _followupRepository;
        private readonly IUnitOfWok _unitOfWok;

        public CustomerFollowupService(IRepository<Customer> customerRepository,
            IRepository<BuildingDetails> buildingRepository,
            IRepository<ContractDetails> contractRepository,
            IRepository<Followup> followupRepository,
            IUnitOfWok unitOfWok)
        {
            this._customerRepository = customerRepository;
            this._buildingDetailsRepository = buildingRepository;
            this._contractDetailsRepository = contractRepository;
            this._followupRepository = followupRepository;
            this._unitOfWok = unitOfWok;
        }
        public async Task<int> AddEntity(CustomerFollowupDto entity)
        {
            _unitOfWok.BeginTransaction();
            try
            {
                Customer customer = new Customer();
                customer.ClientName = entity.Name;
                customer.Address = entity.Address;
                customer.SubAreaId = entity.SubAreaId;
                customer.NoOfFloor = entity.NoOfFloor;
                customer.NoOfFlat = entity.NoOfFlat;
                customer.ContactId = entity.ContactId;
                customer.EmployeeId = entity.EmployeeId;
                customer.ModifiedBy = entity.ModifiedBy;
                customer.CategoryId = entity.CategoryId;
                customer.ReferenceBy = entity.ReferenceBy;

                int Id = await _customerRepository.AddEntity(customer);
                await _unitOfWok.SaveChangesAsync();

                foreach (var item in entity.BuildingDetails)
                {
                    BuildingDetails buildingDetails = new BuildingDetails();
                    buildingDetails.BrandId = item.BrandId;
                    buildingDetails.Quantity = item.Quantity;
                    buildingDetails.Capacity = item.Capacity.ToString();
                    buildingDetails.CustomerId = customer.Id;
                    buildingDetails.ModifiedBy=customer.ModifiedBy;
                    await _buildingDetailsRepository.AddEntity(buildingDetails);
                }
                foreach (var item in entity.ContractDetails)
                {
                    ContractDetails contractDetails = new ContractDetails();
                    contractDetails.Name = item.Name;
                    contractDetails.MobileNo = item.MobileNo;
                    contractDetails.DesignationId = item.DesignationId;
                    contractDetails.CustomerId = customer.Id;
                    contractDetails.ModifiedBy = customer.ModifiedBy;
                    await _contractDetailsRepository.AddEntity(contractDetails);
                }
                Followup followup = new Followup();
                followup.CallingDate = entity.CallingDate;
                followup.OfferAmount = entity.OfferAmount;
                followup.AgreeAmount = entity.AgreeAmount;
                followup.CustomerDoTheWorkingMonth = entity.CustomerDoTheWorkingMonth;
                followup.Remarks = entity.Remarks;
                followup.PositiveOrNegative = entity.PositiveOrNegative;
                followup.DiscussionDetailsNote = entity.DiscussionDetailsNote;
                followup.MarketingNextPlan = entity.MarketingNextPlan;
                followup.FollowupCallDate = entity.FollowupCallDate;
                followup.Status = entity.Status;
                followup.CustomerId = customer.Id;
                followup.ServiceTypeId = entity.ServiceTypeId;
                followup.ModifiedBy = customer.ModifiedBy;
                followup.FollowupById = customer.EmployeeId;

                await _followupRepository.AddEntity(followup);
                await _unitOfWok.SaveChangesAsync();
                _unitOfWok.CommitTransaction();
                return followup.Id;
            }
            catch (Exception ex)
            {
                _unitOfWok.RollbackTransaction();
                Console.WriteLine(ex.ToString());
                return 0;
            }
        }

        public Task<bool> DeleteEntity(object id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CustomerFollowupDto> Find(Expression<Func<CustomerFollowupDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerFollowupDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerFollowupDto> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsRecordExistsAsync(Expression<Func<CustomerFollowupDto, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEntity(CustomerFollowupDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
