using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and confirm password do not match..Please Check")]
        public string ConfirmPassword { get; set; }
    }
}
