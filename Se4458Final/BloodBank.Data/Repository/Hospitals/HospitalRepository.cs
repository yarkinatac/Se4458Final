using BloodBank.Data.Models;
using BloodBank.Data.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodBank.Data.Repository.Hospitals;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BloodBank.Data.Repository.Hospitals
{
    public class HospitalRepository: BaseRepository<Hospital> , IHospitalRepository 
    {
    }
}
