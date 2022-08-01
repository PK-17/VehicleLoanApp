using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleLoanApp.Models
{
    public partial class ApplicantDetail
    {
        public ApplicantDetail()
        {
            EmploymentDetails = new HashSet<EmploymentDetail>();
            IdentityDocuments = new HashSet<IdentityDocument>();
            LoanDetails = new HashSet<LoanDetail>();
            LoanSchemes = new HashSet<LoanScheme>();
            VehicleDetails = new HashSet<VehicleDetail>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public long ContactNo { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public int CustomerId { get; set; }

        public virtual ICollection<EmploymentDetail> EmploymentDetails { get; set; }
        public virtual ICollection<IdentityDocument> IdentityDocuments { get; set; }
        public virtual ICollection<LoanDetail> LoanDetails { get; set; }
        public virtual ICollection<LoanScheme> LoanSchemes { get; set; }
        public virtual ICollection<VehicleDetail> VehicleDetails { get; set; }
    }
}
