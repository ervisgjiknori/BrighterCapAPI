using System;
using System.Collections.Generic;

#nullable disable

namespace BrighterCapAPI.Models
{
    public partial class Valuation
    {
        public string ParcelId { get; set; }
        public decimal BuildingValue { get; set; }
        public DateTime ValuationDate { get; set; }
        public decimal LandValue { get; set; }
        public decimal AppraisedValue { get; set; }
    }
}
