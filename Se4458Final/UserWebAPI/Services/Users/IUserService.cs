using Azure;
using User.Data.Models;
using User.Data;
using Donor.Data.Models;
using User.Data.Models.dto;
using BloodBank.Data.Models;
using User.Data.Models.dto.UserAndBranch;

namespace UserWebAPI.Services.Users
{
    public interface IUserService
    {
        public string CreateTokenByUserRole(int userId, string username, string? hospitalName);
        public Task<BloodBank.Data.Response<UserAndBranchDto>> UpdateUserAndCompleteRegisterOrDeleteIfFailedToBranch(int userId, User.Data.Models.User user, Branch branch);
        public Task<BloodBank.Data.Response<UserAndHospitalDto>> UpdateUserAndCompleteRegisterOrDeleteIfFailedToHospital(int userId, User.Data.Models.User user, Hospital hospital);
        public bool CheckUserExistByEmail(string email);
    }
}
