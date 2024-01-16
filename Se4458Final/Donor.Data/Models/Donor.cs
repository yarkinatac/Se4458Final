using System;
using System.Collections.Generic;

namespace Donor.Data.Models
{
    public class Donor
    {
        public int DonorID { get; set; }
        public int BranchID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string BloodType { get; set; }
        public string PhoneNumber { get; set; }
        public int City { get; set; }
        public int Town { get; set; }
    
        public virtual Branch Branch { get; set; }
        public virtual ICollection<DonationHistory> DonationHistories { get; set; }
    }
}
