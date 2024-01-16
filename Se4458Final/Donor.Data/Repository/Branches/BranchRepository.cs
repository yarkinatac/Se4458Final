using BloodBank.Data.Repository.BaseRepository;
using Donor.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data.Repository.BaseRepository;
using Donor.Data.Models;

using Donor.Data.Repository.Branches;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Donor.Data.Repository.Branches
{
    public class BranchRepository : BaseRepository<Branch>, IBranchRepository
    {
        DonorDBContext _context = new DonorDBContext();

        private DbSet<Branch> query { get; set; }
        public BranchRepository()
        {
            query = _context.Set<Branch>();
        }
        public bool CheckBranchIfExists(int branchId)
        {
            bool isExist = query.Any(p => p.BranchID == branchId);
            if (isExist)
            {
                return true;
            }
            return false;
        }
    }
}
