using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleLoanApp.Models
{
    public partial class LoanScheme
    {
        public int SchemeId { get; set; }
        public string SchemeName { get; set; }
        public decimal MaxLoanAmount { get; set; }
        public int InterestRate { get; set; }
        public decimal Emi { get; set; }
        public decimal ProcessingFee { get; set; }
        public string AccountType { get; set; }
        public int CustomerId { get; set; }

        public virtual ApplicantDetail Customer { get; set; }
    }
}
