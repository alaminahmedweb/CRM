using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Administration
{
    public class RoleVM
    {
        [Required]
        public string RoleName { get; set; }

    }
}
