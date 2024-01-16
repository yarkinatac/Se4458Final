using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor.Data.Models.dto.Donor.dto
{
    public class DonorPhotoDto
    {
        public IFormFile DonorPhoto { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public int DonorID { get; set; } 


    }
}
