using BloodBank.Data.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Data.Models;


namespace User.Data.Repository.Users
{
    public interface IUserRepository : IBaseRepository<Models.User> 
    { 
    }
}