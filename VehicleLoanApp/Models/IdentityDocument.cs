using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleLoanApp.Models
{
    public partial class IdentityDocument
    {
        public int IdentityId { get; set; }
        public int Adharcard { get; set; }
        public int Pancard { get; set; }
        public int Photo { get; set; }
        public int Salaryslip { get; set; }
        public int CustomerId { get; set; }

        public virtual ApplicantDetail Customer { get; set; }
    }
}
