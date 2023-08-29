using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblCountry
    {
        public int CountryId { get; set; }
        public int RegionId { get; set; }
        public string CountryName { get; set; }
    }
}
