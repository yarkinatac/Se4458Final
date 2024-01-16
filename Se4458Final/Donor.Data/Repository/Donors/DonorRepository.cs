using BloodBank.Data.Repository.BaseRepository;
using Donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data.Repository.BaseRepository;

namespace Donor.Data.Repository.Donors
{

    public class DonorRepository : BaseRepository<Models.Donor>, IDonorRepository
    {
    }
}
