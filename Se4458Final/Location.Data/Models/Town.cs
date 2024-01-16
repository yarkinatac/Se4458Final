using System;
using System.Collections.Generic;

namespace Location.Data.Models
{
    public partial class Town
    {
        public int TownID { get; set; }
        public string Name { get; set; } = null!;
    }
}
