

namespace User.Logic.Logics.Users
{
    public interface IUserLogic
    {
        public bool Add(Data.Models.User entity);
        public int AddAndGetId(Data.Models.User entity);
        public bool Delete(int id);
        public bool DeleteSingleByMethod(int id);
        public bool DeleteList(int id);
        public Data.Models.User? GetSingle(int id);
        public Data.Models.User? GetSingleByMethod(int id);
        public Data.Models.User? GetSingleByMethod(string email);
        public List<Data.Models.User>? GetList(int id);
        public Task<Data.Models.User?> UpdateAsync(int id, Data.Models.User updatedEntity);
        public Task<Data.Models.User?> UpdateAsync(Data.Models.User entity, Data.Models.User updatedEntity);
    }
}