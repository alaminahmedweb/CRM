using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Administration
{
    public class EditRoleVM
    {
        public EditRoleVM()
        {
            Users = new List<string>();
        }
        public string Id { get; set; }

        [Required(ErrorMessage ="Role Name is required")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}
