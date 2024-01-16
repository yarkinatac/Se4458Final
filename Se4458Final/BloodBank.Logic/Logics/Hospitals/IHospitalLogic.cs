using BloodBank.Data.Models;


namespace BloodBank.Logic.Logics.Hospitals
{
    public interface IHospitalLogic
    {
        public bool AddHospital(Hospital entity);
        public int AddAndGetIdHospital(Hospital entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);

        public bool DeleteList(int id);
        public Hospital? GetSingle(int id);
        public Hospital? GetSingleByMethod(int id);
        public Hospital? GetSingleByMethodName(string name);
        public Hospital? GetSingleByMethodGeopoint(int id);

        public List<Hospital>? GetListHospitals(int id);
        public Task<Hospital?> UpdateAsyncHospitals(int id, Hospital updatedEntity);
        public Task<Hospital?> UpdateAsyncHospital(Hospital entity, Hospital updatedEntity);
        
    }
}
