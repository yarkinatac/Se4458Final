using System;
using System.Collections.Generic;

namespace Donor.Data.Models
{
    public class DonationHistory
    {
        public int HistoryID { get; set; }
        public int DonorID { get; set; }
        public int TupleNumber { get; set; }
        public DateTime DonationDate { get; set; } 
    
        public virtual Donor Donor { get; set; }
    }
}
