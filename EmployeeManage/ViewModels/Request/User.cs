using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManage.ViewModels.Request
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime Passwordexp { get; set; }
        public string UserGuid { get; set; }
        public string Role { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }

    }
}
