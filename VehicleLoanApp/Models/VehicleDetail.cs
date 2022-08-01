using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleLoanApp.Models
{
    public partial class VehicleDetail
    {
        public int VehicleId { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public decimal? ExshowroomPrice { get; set; }
        public decimal? OnroadPrice { get; set; }
        public int CustomerId { get; set; }

        public virtual ApplicantDetail Customer { get; set; }
    }
}
