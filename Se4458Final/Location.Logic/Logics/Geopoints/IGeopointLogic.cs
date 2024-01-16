using Location.Data.Models;

namespace Location.Logic.Logics.Geopoints
{
    public interface IGeopointLogic
    {
        public bool Add(Geopoint entity);
        public int AddAndGetId(Geopoint entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);
        public bool DeleteList(int id);
        public Geopoint? GetSingle(int id);
        public Geopoint? GetSingleByMethod(int id);
        public Geopoint? GetSingleByMethods(double lat, double longitude);
        public List<Geopoint>? GetList(int id);
        public Task<Geopoint?> UpdateAsync(int id, Geopoint updatedEntity);
        public Task<Geopoint?> UpdateAsync(Geopoint entity, Geopoint updatedEntity);
    }
}
