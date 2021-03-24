using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrighterCapAPI.Models
{
    public partial class LastValuation
    {
        public string ParcelId { get; set; }
        public DateTime DateOfListing { get; set; }
        public int NumberOfYears { get; set; }
        public decimal TaxesOwed { get; set; }
        public string CurrentRecordHolder { get; set; }
        public string DefendantInFifa { get; set; }
        public string Category { get; set; }
        public string County { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertyClass { get; set; }
        public string TaxYearsDue { get; set; }
        public string MailingAddress { get; set; }
        public decimal? BuildingValue { get; set; }
        public DateTime? ValuationDate { get; set; }
        public decimal? LandValue { get; set; }
        public decimal? AppraisedValue { get; set; }
        public long RowNum { get; set; }
    }
}
