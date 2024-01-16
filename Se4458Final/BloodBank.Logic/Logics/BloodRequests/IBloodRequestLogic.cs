using BloodBank.Data.Models;


namespace BloodBank.Logic.Logics.BloodRequests
{
    public interface IBloodRequestLogic
    {
        public bool AddReq(RequestForBlood entity);

        public int AddAndGetIdReq(RequestForBlood entity);

        public bool DeleteReq(int id);

        public bool DeleteSingleByMethod(int id);
        
        public bool DeleteList(int id);

        public RequestForBlood? GetSingleReq(int id);

        public RequestForBlood? GetSingleByMethodReq(int id);
        
        public List<RequestForBlood>? GetListRequests(int id);

         public Task<RequestForBlood?> UpdateAsync(int id, RequestForBlood updatedEntity);

        public Task<RequestForBlood?> UpdateAsync(RequestForBlood entity, RequestForBlood updatedEntity);

    }
}
