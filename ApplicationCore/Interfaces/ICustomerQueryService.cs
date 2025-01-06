using ApplicationCore.DtoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICustomerQueryService
    {
        bool IsMobileNoAlreadyExists(string mobileNo,int customerId);
        IEnumerable<AllCustomerListDto> GetAllCustomers();
        IEnumerable<AllCustomerListDto> GetAllCustomersByMobileNo(string term);
        CustomerDto GetCustomerById(int customerId);
        IEnumerable<AllCustomerListDto> GetAllCustomersBySearchString(string customerName,
            string address, string mobileNo);
    }
}
