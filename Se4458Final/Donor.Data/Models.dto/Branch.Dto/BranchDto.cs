using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor.Data.Models.dto.Branch.Dto
{
    public class BranchDto
    {
        public int BranchID { get; set; }
        public string City { get; set; } = null!;
        public string Town { get; set; } = null!;
        public int APlusBlood { get; set; }
        public int AMinusBlood { get; set; }
        public int BPlusBlood { get; set; }
        public int BMinusBlood { get; set; }
        public int AbPlusBlood { get; set; }
        public int AbMinusBlood { get; set; }
        public int ZeroPlusBlood { get; set; }
        public int ZeroMinusBlood { get; set; }
        public int GeopointID { get; set; }
    }
}
