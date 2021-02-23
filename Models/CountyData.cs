using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BrighterCapAPI.Models
{
    public partial class CountyData
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
        public decimal? Ltv { get; set; }
        public string TaxYearsDue { get; set; }
        public string MailingAddress { get; set; }
    }
}
