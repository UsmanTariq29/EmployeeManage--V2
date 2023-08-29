using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblEmployeeAllowance
    {
        public int AllowanceId { get; set; }
        public decimal Amount { get; set; }
        public string AllowanceDetail { get; set; }
        public int EmployeeId { get; set; }

        public virtual TblEmployee Employee { get; set; }
    }
}
