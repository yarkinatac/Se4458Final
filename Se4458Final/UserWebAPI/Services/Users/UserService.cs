using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using Azure;
using BloodBank.Data.Models;
using Donor.Data.Models;
using Location.Data.Models;
using Location.Logic.Logics.Cities;
using Location.Logic.Logics.Towns;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using User.Data;
using User.Data.Models;
using User.Data.Models.dto;
using User.Data.Models.dto.UserAndBranch;
using User.Logic.Logics.Users;
using UserWebAPI.Services.Jwt;
using UserWebAPI.Services.Users;

namespace UserWebAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserLogic _userLogic;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;
        private readonly ICityLogic _cityLogic;
        private readonly ITownLogic _townLogic;

        public UserService(IUserLogic userLogic, IMapper mapper, IJwtService jwtService, IConfiguration configuration, ICityLogic cityLogic, ITownLogic townLogic)
        {
            _userLogic = userLogic;
            _mapper = mapper;
            _jwtService = jwtService;
            _configuration = configuration;
            _cityLogic = cityLogic;
            _townLogic = townLogic;
        }

        public string CreateTokenByUserRole(int userId , string username, string? hospitalName)
        {
            if (hospitalName.IsNullOrEmpty())
            {
                return _jwtService.CreateToken(userId,username,"branchemployee",_configuration);
            }
            return _jwtService.CreateToken(userId, username, "branchemployee",_configuration);
        }

        public async Task<BloodBank.Data.Response<UserAndBranchDto>> UpdateUserAndCompleteRegisterOrDeleteIfFailedToBranch(int userId, User.Data.Models.User user,Branch branch)
        {
            User.Data.Models.User? updatedUser = await _userLogic.UpdateAsync(userId, user);
            if (updatedUser == null)
            {
                bool isDeleted = _userLogic.Delete(userId);
                if (isDeleted)
                {
                    return new BloodBank.Data.Response<UserAndBranchDto> { Message = "User deleted successfully", Data = new UserAndBranchDto(), Progress = false };
                }
                return new BloodBank.Data.Response<UserAndBranchDto> { Message = "Error", Data = new UserAndBranchDto(), Progress = false };
            }
            return new BloodBank.Data.Response<UserAndBranchDto> { Message = "Successfully added", Data = new UserAndBranchDto() { User = user, Branch = branch}, Progress = true };
        }
        public async Task<BloodBank.Data.Response<UserAndHospitalDto>> UpdateUserAndCompleteRegisterOrDeleteIfFailedToHospital(int userId, User.Data.Models.User user, Hospital hospital)
        {
            User.Data.Models.User? updatedUser = await _userLogic.UpdateAsync(userId, user);
            if (updatedUser == null)
            {
                bool isDeleted = _userLogic.Delete(userId);
                if (isDeleted)
                {
                    return new BloodBank.Data.Response<UserAndHospitalDto> { Message = "Successfully deleted", Data = new UserAndHospitalDto(), Progress = false };
                }
                return new BloodBank.Data.Response<UserAndHospitalDto> { Message = "Error", Data = new UserAndHospitalDto(), Progress = false };
            }
            return new BloodBank.Data.Response<UserAndHospitalDto> { Message = "User successfully added", Data = new UserAndHospitalDto() { User = user, Hospital = hospital }, Progress = true };
        }
        public bool CheckUserExistByEmail(string email)
        {
            User.Data.Models.User? user = _userLogic.GetSingleByMethod(email);
            if(user != null)
            {
                return true;
            }
            return false;
        }

    }
}
