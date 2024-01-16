using BloodBank.Data.Repository.BaseRepository;
using Location.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data.Repository.BaseRepository;

namespace Location.Data.Repository.Geopoints
{
    public class GeopointRepository : BaseRepository<Geopoint>, IGeopointRepository
    {
    }
}
