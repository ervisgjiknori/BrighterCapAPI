using System;
using System.Collections.Generic;

#nullable disable

namespace BrighterCapAPI.Models
{
    public partial class Sales
    {
        public string ParcelId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal SalePrice { get; set; }
    }
}
