using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Remember me?")]
        public bool RememberMe { get; set; }
    }
}
