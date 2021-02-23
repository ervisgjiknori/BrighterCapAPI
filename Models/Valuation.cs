using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrighterCapAPI.Models
{
    public partial class Valuation
    {
        public string ParcelId { get; set; }
        public decimal? BuildingValue { get; set; }
        public DateTime ValuationDate { get; set; }
        public decimal? LandValue { get; set; }
        public decimal? AppraisedValue { get; set; }
    }
}
