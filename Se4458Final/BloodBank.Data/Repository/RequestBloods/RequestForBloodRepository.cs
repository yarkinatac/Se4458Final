using BloodBank.Data.Models;
using BloodBank.Data.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Data.Repository.RequestBloods
{
    public class BloodRequestRepository : BaseRepository<RequestForBlood>, IRequestForBloodRepository
    {
    }
}