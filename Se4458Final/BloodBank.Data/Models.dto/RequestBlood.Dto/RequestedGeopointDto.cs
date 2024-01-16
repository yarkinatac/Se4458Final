using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Data.Models.dto.RequestBlood.Dto
{
    public class RequestedGeopointDto
    {
        public GeopointDto HospitalGeopoint { get; set; } = null!;
        public List<GeopointDto> RequestedBloodLine { get; set; } = null!;
    }
}
