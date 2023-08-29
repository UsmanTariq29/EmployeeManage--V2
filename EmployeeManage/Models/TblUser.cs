using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblUser
    {
        public int Id { get; set; }
        public string UserGuid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Passwordexp { get; set; }
        public string Role { get; set; }
        public string CompanyGuid { get; set; }
        public string BranchGuid { get; set; }
    }
}
