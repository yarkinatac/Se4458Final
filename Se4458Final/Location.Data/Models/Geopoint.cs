using System;
using System.Collections.Generic;

namespace Location.Data.Models
{
    public partial class Geopoint
    {
        public int GeopointID { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
