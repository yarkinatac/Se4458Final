using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Data.Models.dto.RequestBlood.Dto
{
    public class BloodRequestAndBranchDto
    {
        public int BloodNumber { get; set; }
        public string BloodType { get; set; } = null!;
        public DateTime DurationDate { get; set; }
        public string City { get; set; } = null!;
        public string Town { get; set; } = null!;
        public int GeopointID { get; set; } 
        public string Email { get; set; } = null!;
    }
}
