using ApplicationCore.Entities;

namespace Web.ViewModels
{
    public class SearchCustomerVM
    {
        public SearchCustomerVM()
        {
            BuildingDetails = new List<BuildingDetailsVM>();
            ContractDetails = new List<ContractDetailsVM>();
        }

        public int ServiceTypeId { get; set; }

        public string? Name { get; set; } = String.Empty;

        public string? MobileNo { get; set; } = String.Empty;
        public string? Address { get; set; } = String.Empty;

        public int SubAreaId { get; set; } //FK
        public int ContactId { get; set; } //FK
        public int EmployeeId { get; set; }//FK

        public List<BuildingDetailsVM>? BuildingDetails { get; set; }
        public List<ContractDetailsVM>? ContractDetails { get; set; }

        public int CustomerDoTheWorkingMonth { get; set; }

        public int CustomerId { get; set; } = 0;//FK

        public int CategoryId { get; set; }
        public string ReferenceBy { get; set; }

        public IEnumerable<Designation>? DesignationList { get; set; }
        public IEnumerable<City>? CityList { get; set; }
        public IEnumerable<ContactBy>? ContactList { get; set; }
        public IEnumerable<Employee>? EmployeeList { get; set; }
        public IEnumerable<MonthList>? MonthList { get; set; }
        public IEnumerable<ServiceType>? ServiceList { get; set; }
        public IEnumerable<Brand>? BrandList { get; set; }
        public IEnumerable<Category>? CategoryList { get; set; }

    }
}
