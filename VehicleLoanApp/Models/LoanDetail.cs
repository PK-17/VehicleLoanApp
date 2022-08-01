using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleLoanApp.Models
{
    public partial class LoanDetail
    {
        public int LoanId { get; set; }
        public decimal LoanAmount { get; set; }
        public int LoanTenure { get; set; }
        public int LoanInterestRate { get; set; }
        public int StatusId { get; set; }
        public int CustomerId { get; set; }

        public virtual ApplicantDetail Customer { get; set; }
        public virtual LoanApplicationStatus Status { get; set; }
    }
}
