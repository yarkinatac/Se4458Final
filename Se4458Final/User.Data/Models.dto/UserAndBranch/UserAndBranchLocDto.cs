﻿using Donor.Data.Models;
using Donor.Data.Models.dto.Branch.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Data.Models.dto.UserAndBranch
{
    public class UserAndBranchLocDto
    {
  
            public User User { get; set; } = null!;
            public BranchDto Branch { get; set; } = null!;
        
    }
}
