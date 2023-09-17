using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class CustomerVM
    {
        public CustomerVM()
        {
            BuildingDetails = new List<BuildingDetailsVM>();
            ContractDetails = new List<ContractDetailsVM>();
        }
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; } = String.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Name at least 3 Character")]
        public string ClientName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Address is required")]
        [MinLength(5, ErrorMessage = "Address at least 5 Character")]
        public string? Address { get; set; } = String.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Please Select Sub Area")]
        public int SubAreaId { get; set; } //FK
        public int NoOfFloor { get; set; } = 0;
        public int NoOfFlat { get; set; } = 0;
        [Range(0, int.MaxValue, ErrorMessage = "Please Select Contact")]
        public int ContactId { get; set; } //FK
        [Range(0, int.MaxValue, ErrorMessage = "Please Select Marketing Officer")]
        public int EmployeeId { get; set; }//FK
        public string? ModifiedBy { get; set; } = "";
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public int CityId { get; set; }
        public int AreaId { get; set; }

        public List<BuildingDetailsVM>? BuildingDetails { get; set; }
        public List<ContractDetailsVM>? ContractDetails { get; set; }


        [Range(0, int.MaxValue, ErrorMessage = "Please Select Category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please select Reference By")]
        public string ReferenceBy { get; set; }

        public IEnumerable<Designation>? DesignationList { get; set; }
        public IEnumerable<City>? CityList { get; set; }
        public IEnumerable<Area>? AreaList { get; set; }
        public IEnumerable<SubArea>? SubAreaList { get; set; }
        public IEnumerable<ContactBy>? ContactList { get; set; }
        public IEnumerable<Employee>? EmployeeList { get; set; }
        public IEnumerable<Brand>? BrandList { get; set; }
        public IEnumerable<Category>? CategoryList { get; set; }

    }
}
