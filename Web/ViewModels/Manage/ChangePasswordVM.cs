using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Manage
{
    public class ChangePasswordVM
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="The New Password and old password do not match..Please Check..")]
        public string ConfirmPassword { get; set; }

    }
}
