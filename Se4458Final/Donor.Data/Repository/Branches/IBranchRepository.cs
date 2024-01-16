using BloodBank.Data.Repository.BaseRepository;
using Donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Donor.Data.Repository.Branches
{
    public interface IBranchRepository : IBaseRepository<Branch>
    {
        public bool CheckBranchIfExists(int branchId);
    }
}
