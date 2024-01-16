using Location.Data.Models;


namespace Location.Logic.Logics.Towns
{
    public interface ITownLogic
    {
        public bool Add(Town entity);
        public int AddAndGetId(Town entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);
        public Town? GetSingleByMethod(string name);
        
        public bool DeleteList(int id);
        public Town? GetSingle(int id);
        public Town? GetSingleByMethod(int id);
        public List<Town>? GetList(int id);
        public Task<Town?> UpdateAsync(int id, Town updatedEntity);
        public Task<Town?> UpdateAsync(Town entity, Town updatedEntity);
    }
}
