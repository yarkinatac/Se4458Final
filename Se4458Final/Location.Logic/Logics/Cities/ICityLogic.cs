using Location.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Logic.Logics.Cities
{
    public interface ICityLogic
    {
        public bool Add(City entity);
        public int AddAndGetId(City entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);
        public City? GetSingleByMethod(string name);
        public bool DeleteList(int id);
        public City? GetSingle(int id);
        public City? GetSingleByMethod(int id);
        public List<City>? GetList(int id);
        public Task<City?> UpdateAsync(int id, City updatedEntity);
        public Task<City?> UpdateAsync(City entity, City updatedEntity);
    }
}