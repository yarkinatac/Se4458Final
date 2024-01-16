using System;
using System.Collections.Generic;

namespace BloodBank.Data.Models
{
    public partial class Hospital
    {
        public int HospitalID { get; set; }
        public string Name { get; set; } = null!;
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
