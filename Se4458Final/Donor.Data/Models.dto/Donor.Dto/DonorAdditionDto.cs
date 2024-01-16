using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor.Data.Models.dto.Donor.Dto
{
    public class DonorAdditionDto
    {
        public int BranchID { get; set; } 
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string BloodType { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Town { get; set; } = null!;
        public IFormFile DonorPhoto { get; set; } = null!;
    }
}
