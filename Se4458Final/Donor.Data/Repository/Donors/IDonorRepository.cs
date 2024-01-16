using BloodBank.Data.Repository.BaseRepository;
using Donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor.Data.Repository.Donors
{
    public interface IDonorRepository : IBaseRepository<Models.Donor>
    {
    }
}
