using System;
using System.Collections.Generic;

#nullable disable

namespace EmployeeManage.Models
{
    public partial class TblItemBarcode
    {
        public int BarcodeId { get; set; }
        public int ItemId { get; set; }
        public string ItemBarcode { get; set; }
    }
}
