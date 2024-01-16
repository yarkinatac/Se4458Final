using BloodBank.Data.Repository.BaseRepository;
using Location.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Location.Data.Repository.Cities;


namespace Location.Data.Repository.Cities
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
    }
}