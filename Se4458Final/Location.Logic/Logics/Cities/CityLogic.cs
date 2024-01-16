using Location.Data.Models;
using Location.Data.Repository.Cities;

namespace Location.Logic.Logics.Cities
{
    public class CityLogic : ICityLogic
    {
        private ICityRepository _repository;
        public CityLogic(ICityRepository repository)
        {
            _repository = repository;
        }
        public bool Add(City entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(City entity)
        {
            int addResult = _repository.AddAndGetId(entity);
            return addResult;
        }
        public bool Delete(int id)
        {
            bool deleteResult = _repository.Delete(id);
            return deleteResult;
        }
        public bool DeleteSingleByMethod(int id)
        {
            Func<City, bool> filter = filter => filter.CityID == id;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }
        public bool DeleteList(int id)
        {
            Func<City, bool> filter = filter => filter.CityID == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public City? GetSingle(int id)
        {
            City? result = _repository.GetSingle(id);
            return result;
        }
        public City? GetSingleByMethod(int id)
        {
            Func<City, bool> filter = filter => filter.CityID == id;
            City? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public City? GetSingleByMethod(string name)
        {
            Func<City, bool> filter = filter => filter.Name == name;
            City? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public List<City>? GetList(int id)
        {
            Func<City, bool> filter = filter => filter.CityID == id;
            var list = _repository.GetList(filter);
            return list;
        }
        public async Task<City?> UpdateAsync(int id, City updatedEntity)
        {
            Func<City, bool> filter = filter => filter.CityID == id;
            City? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<City?> UpdateAsync(City entity, City updatedEntity)
        {
            City? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
    }
}