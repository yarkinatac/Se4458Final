using System;
using System.Collections.Generic;

namespace Donor.Data.Models
{
    public partial class Branch
    {
        public int BranchID { get; set; }
        public int City { get; set; }
        public int Town { get; set; }
        public int APlusBlood { get; set; }
        public int AMinusBlood { get; set; }
        public int BPlusBlood { get; set; }
        public int BMinusBlood { get; set; }
        public int AbPlusBlood { get; set; }
        public int AbMinusBlood { get; set; }
        public int ZeroPlusBlood { get; set; }
        public int ZeroMinusBlood { get; set; }
        public int GeopointID { get; set; }
        
        public virtual ICollection<Donor> Donors { get; set; }

    }
}
