using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleLoanApp.Models
{
    public partial class IdentityDocument
    {
        public int IdentityId { get; set; }
        public byte[] Adharcard { get; set; }
        public byte[] Pancard { get; set; }
        public byte[] Photo { get; set; }
        public byte[] Salaryslip { get; set; }
        public int CustomerId { get; set; }

        public virtual ApplicantDetail Customer { get; set; }
    }
}
