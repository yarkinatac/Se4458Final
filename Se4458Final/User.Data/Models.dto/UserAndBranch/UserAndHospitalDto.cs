using BloodBank.Data.Models;
using Donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Data.Models.dto.UserAndBranch
{
    public class UserAndHospitalDto
    {
        public User User { get; set; } = null!;
        public Hospital Hospital { get; set; } = null!;
    }
}
