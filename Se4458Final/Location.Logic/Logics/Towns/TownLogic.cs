using Location.Data.Models;
using Location.Data.Repository.Towns;


namespace Location.Logic.Logics.Towns
{
    public class TownLogic : ITownLogic
    {
        private ITownRepository _repository;
        public TownLogic(ITownRepository repository)
        {
            _repository = repository;
        }
        public bool Add(Town entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(Town entity)
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
            Func<Town, bool> filter = filter => filter.TownID == id;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }
        public bool DeleteList(int id)
        {
            Func<Town, bool> filter = filter => filter.TownID == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public Town? GetSingle(int id)
        {
            Town? result = _repository.GetSingle(id);
            return result;
        }
        public Town? GetSingleByMethod(int id)
        {
            Func<Town, bool> filter = filter => filter.TownID == id;
            Town? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public Town? GetSingleByMethod(string name)
        {
            Func<Town, bool> filter = filter => filter.Name == name;
            Town? result = _repository.GetSingleByMethod(filter);
            return result;
        }

        public List<Town>? GetList(int id)
        {
            Func<Town, bool> filter = filter => filter.TownID == id;
            var list = _repository.GetList(filter);
            return list;
        }
        public async Task<Town?> UpdateAsync(int id, Town updatedEntity)
        {
            Func<Town, bool> filter = filter => filter.TownID == id;
            Town? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<Town?> UpdateAsync(Town entity, Town updatedEntity)
        {
            Town? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
    }
}
