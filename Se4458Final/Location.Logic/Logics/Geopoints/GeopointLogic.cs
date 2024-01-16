using Location.Data.Models;
using Location.Data.Repository.Cities;
using Location.Data.Repository.Geopoints;

namespace Location.Logic.Logics.Geopoints
{
    public class GeopointLogic : IGeopointLogic
    {
        private IGeopointRepository _repository;
        public GeopointLogic(IGeopointRepository repository)
        {
            _repository = repository;
        }
        public bool Add(Geopoint entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(Geopoint entity)
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
            Func<Geopoint, bool> filter = filter => filter.GeopointID == id;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }

        public Geopoint? GetSingleByMethods(double lat,double longitude)
        {
            Func<Geopoint, bool> filter = filter => filter.Latitude == lat;
            Func<Geopoint, bool> filter2 = filter => filter.Longitude == longitude;
            Geopoint? result = _repository.GetSingleByMethod(filter, filter2);
            return result;
        }
        
        public bool DeleteList(int id)
        {
            Func<Geopoint, bool> filter = filter => filter.GeopointID == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public Geopoint? GetSingle(int id)
        {
            Geopoint? result = _repository.GetSingle(id);
            return result;
        }
        public Geopoint? GetSingleByMethod(int id)
        {
            Func<Geopoint, bool> filter = filter => filter.GeopointID == id;
            Geopoint? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public List<Geopoint>? GetList(int id)
        {
            Func<Geopoint, bool> filter = filter => filter.GeopointID == id;
            var list = _repository.GetList(filter);
            return list;
        }
        public async Task<Geopoint?> UpdateAsync(int id, Geopoint updatedEntity)
        {
            Func<Geopoint, bool> filter = filter => filter.GeopointID == id;
            Geopoint? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<Geopoint?> UpdateAsync(Geopoint entity, Geopoint updatedEntity)
        {
            Geopoint? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
    }
}
