using BloodBank.Data.Models;
using BloodBank.Data.Repository.RequestBloods;

namespace BloodBank.Logic.Logics.BloodRequests
{
    public class BloodRequestLogic : IBloodRequestLogic
    {
        private IRequestForBloodRepository _repository;
        public BloodRequestLogic(IRequestForBloodRepository repository)
        {
            _repository = repository;
        }
        public bool AddReq(RequestForBlood entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetIdReq(RequestForBlood entity)
        {
            int addResult = _repository.AddAndGetId(entity);
            return addResult;
        }
        public bool DeleteReq(int id)
        {
            bool deleteResult = _repository.Delete(id);
            return deleteResult;
        }
        public bool DeleteSingleByMethod(int id)
        {
            Func<RequestForBlood, bool> filter = filter => filter.RequestID == id;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }

        public bool DeleteList(int id)
        {
            Func<RequestForBlood, bool> filter = filter => filter.RequestID == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public RequestForBlood? GetSingleReq(int id)
        {
            RequestForBlood? result = _repository.GetSingle(id);
            return result;
        }
        public RequestForBlood? GetSingleByMethodReq(int id)
        {
            Func<RequestForBlood, bool> filter = filter => filter.RequestID == id;
            RequestForBlood? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        
        public List<RequestForBlood>? GetListRequests(int id)
        {
            Func<RequestForBlood, bool> filter = filter => filter.RequestID == id;
            var list = _repository.GetList(filter);
            return list;
        }

        public async Task<RequestForBlood?> UpdateAsync(int id, RequestForBlood updatedEntity)
        {
            Func<RequestForBlood, bool> filter = filter => filter.RequestID == id;
            RequestForBlood? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }

        public async Task<RequestForBlood?> UpdateAsync(RequestForBlood entity, RequestForBlood updatedEntity)
        {
            RequestForBlood? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }

    }
}
