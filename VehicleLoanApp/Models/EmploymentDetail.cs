using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleLoanApp.Models
{
    public partial class EmploymentDetail
    {
        public int EmpId { get; set; }
        public string TypeOfEmployement { get; set; }
        public decimal YearlySalary { get; set; }
        public decimal? ExistingEmi { get; set; }
        public int CustomerId { get; set; }

        public virtual ApplicantDetail Customer { get; set; }
    }
}
