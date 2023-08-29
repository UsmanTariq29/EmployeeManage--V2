using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EmployeeManage.ViewModels
{
    public class LoginUserRequestVM
    {
        [Required]
        public string UserName { get; set; }
        //[Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("ConfirmPassword")]
        [Compare("Password",
            ErrorMessage = "Password and confirmationPassword Do not Match")]
        public string ConfirmPassword { get; set; }
        [DisplayName("RememberMe")]
        public bool RememberMe{ get; set; }
    }
}
