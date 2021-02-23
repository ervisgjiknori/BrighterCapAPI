using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrighterCapAPI.Models
{
    public partial class Sales
    {
        public string ParcelId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal? SalePrice { get; set; }
    }
}
