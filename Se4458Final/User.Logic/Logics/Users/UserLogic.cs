
using User.Data.Repository.Users;

namespace User.Logic.Logics.Users
{
    public class UserLogic : IUserLogic
    {
        private IUserRepository _repository;
        public UserLogic(IUserRepository repository)
        {
            _repository = repository;
        }
        public bool Add(Data.Models.User entity)
        {
            bool addResult = _repository.Add(entity);
            return addResult;
        }
        public int AddAndGetId(Data.Models.User entity)
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
            Func<Data.Models.User, bool> filter = filter => filter.UserID == id;
            bool deleteResult = _repository.DeleteSingleByMethod(filter);
            return deleteResult;
        }
        public bool DeleteList(int id)
        {
            Func<Data.Models.User, bool> filter = filter => filter.UserID == id;
            bool deleteResult = _repository.DeleteList(filter);
            return deleteResult;
        }
        public Data.Models.User? GetSingle(int id)
        {
            Data.Models.User? result = _repository.GetSingle(id);
            return result;
        }
        public Data.Models.User? GetSingleByMethod(int id)
        {
            Func<Data.Models.User, bool> filter = filter => filter.UserID == id;
            Data.Models.User? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        public Data.Models.User? GetSingleByMethod(string email)
        {
            Func<Data.Models.User, bool> filter = filter => filter.Email == email;
            Data.Models.User? result = _repository.GetSingleByMethod(filter);
            return result;
        }
        
        public List<Data.Models.User>? GetList(int id)
        {
            Func<Data.Models.User, bool> filter = filter => filter.UserID == id;
            var list = _repository.GetList(filter);
            return list;
        }
        public async Task<Data.Models.User?> UpdateAsync(int id, Data.Models.User updatedEntity)
        {
            Func<Data.Models.User, bool> filter = filter => filter.UserID == id;
            Data.Models.User? updateResult = await _repository.UpdateAsync(filter, updatedEntity);
            return updateResult;
        }
        public async Task<Data.Models.User?> UpdateAsync(Data.Models.User entity, Data.Models.User updatedEntity)
        {
            Data.Models.User? updateResult = await _repository.UpdateAsync(entity, updatedEntity);
            return updateResult;
        }
    }
}
