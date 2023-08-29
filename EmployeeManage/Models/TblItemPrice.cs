using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblItemPrice
    {
        public int PriceId { get; set; }
        public int ItemId { get; set; }
        public string CompanyGuid { get; set; }
        public decimal ItemsPrice { get; set; }
        public string UserGuid { get; set; }
    }
}
