using BloodBank.Data;
using BloodBank.Data.Models;
using BloodBank.Data.Repository.Hospitals;


namespace BloodBank.Logic.Logics.Hospitals
{
    public class HospitalLogic: IHospitalLogic
    {
        
        private IHospitalRepository _repository;
        public HospitalLogic(IHospitalRepository repository)
        {
            _repository = repository;
        }
        public bool AddHospital(Hospital entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetIdHospital(Hospital entity)
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
            Func<Hospital, bool> filter = filter => filter.HospitalID == id;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }
        
        public bool DeleteList(int id)
        {
            Func<Hospital, bool> filter = filter => filter.HospitalID == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public Hospital? GetSingle(int id)
        {
            Hospital? result = _repository.GetSingle(id);
            return result;
        }
        public Hospital? GetSingleByMethod(int id)
        {
            Func<Hospital, bool> filter = filter => filter.HospitalID == id;
            Hospital? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public Hospital? GetSingleByMethodName(string name)
        {
            Func<Hospital, bool> filter = hospitalItem => hospitalItem.Name == name;
            Hospital? result = _repository.GetSingleByMethod(filter);
            return result;
        }

        public Hospital? GetSingleByMethodGeopoint(int id)
        {
            Func<Hospital, bool> filter = filter => filter.GeopointID == id;
            Hospital? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public List<Hospital>? GetListHospitals(int id)
        {
            Func<Hospital, bool> filter = filter => filter.HospitalID == id;
            var list = _repository.GetList(filter);
            return list;
        }

      

        public async Task<Hospital?> UpdateAsyncHospitals(int id, Hospital updatedEntity)
        {
            Func<Hospital, bool> filter = filter => filter.HospitalID == id;
            Hospital? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<Hospital?> UpdateAsyncHospital(Hospital entity, Hospital updatedEntity)
        {
            Hospital? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
        
    }

}
