using Donor.Data.Models;
using Donor.Data.Repository.Branches;
using Donor.Logic.Logics.Branches;

namespace Donor.Logic.Logics.Branhces
{
    public class BranchLogic : IBranchLogic
    {
        private IBranchRepository _repository;
        public BranchLogic(IBranchRepository repository)
        {
            _repository = repository;
        }
        public bool Add(Branch entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(Branch entity)
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
            Func<Branch, bool> filter = filter => filter.BranchID == id;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }
        public bool DeleteList(int id)
        {
            Func<Branch, bool> filter = filter => filter.BranchID == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public Branch? GetSingle(int id)
        {
            Branch? result = _repository.GetSingle(id);
            return result;
        }
        public Branch? GetSingleByMethod(int id)
        {
            Func<Branch, bool> filter = filter => filter.BranchID == id;
            Branch? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public Branch? GetSingleByMethodGeoPoint(int id)
        {
            Func<Branch, bool> filter = filter => filter.GeopointID == id;
            Branch? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public Branch? GetSingleByMethods(int city, int town)
        {
            Func<Branch, bool> filter = filter => filter.City == city;
            Func<Branch, bool> filter2 = filter => filter.Town == town;
            Branch? result = _repository.GetSingleByMethod(filter,filter2);
            return result;
        }
        public List<Branch>? GetList(int id)
        {
            Func<Branch, bool> filter = filter => filter.BranchID == id;
            var list = _repository.GetList(filter);
            return list;
        }
        public async Task<Branch?> UpdateAsync(int id, Branch updatedEntity)
        {
            Func<Branch, bool> filter = filter => filter.BranchID == id;
            Branch? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<Branch?> UpdateAsync(Branch entity, Branch updatedEntity)
        {
            Branch? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
        public bool CheckExistence(int branchId)
        {
            bool isExist = _repository.CheckBranchIfExists(branchId);
            return isExist;
        }
    }
}
