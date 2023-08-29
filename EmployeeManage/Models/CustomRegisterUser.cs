using Microsoft.AspNetCore.Identity;
using System;

namespace EmployeeManage.Models
{
    public class CustomRegisterUser :IdentityUser 
    {
        public string UserGuid { get; set; }
        public string BranchGuid { get; set; }
        public string CompanyGuid { get; set; }
    }
}
