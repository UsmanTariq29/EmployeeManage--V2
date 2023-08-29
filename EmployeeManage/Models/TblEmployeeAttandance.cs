using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblEmployeeAttandance
    {
        public int AttandanceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AttandanceDate { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
    }
}
