using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleLoanApp.Models
{
    public partial class LoanApplicationStatus
    {
        public LoanApplicationStatus()
        {
            LoanDetails = new HashSet<LoanDetail>();
        }

        public int StatusId { get; set; }
        public string StatusName { get; set; }

        public virtual ICollection<LoanDetail> LoanDetails { get; set; }
    }
}
