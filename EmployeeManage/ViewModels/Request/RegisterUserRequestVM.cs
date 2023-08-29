using System.ComponentModel.DataAnnotations;

namespace EmployeeManage.ViewModels.Request
{
    public class RegisterUserRequestVM
    {
        [Required]
        
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword{ get; set; }
        [Required]
        public string Role{ get; set; }

        
        public string UserGUID { get; set; }

        [Required]
        public string BranchGUID { get; set; }

        [Required]
        public string CompanyGUID { get; set; }

    }
}
