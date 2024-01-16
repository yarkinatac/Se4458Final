using BloodBank.Data.Repository.BaseRepository;
using Donor.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data.Repository.BaseRepository;
using Donor.Data.Models;

namespace Donor.Data.Repository.DonationHistories
{
    public interface IDonationHistoryRepository : IBaseRepository<DonationHistory>
    {
    }
}
