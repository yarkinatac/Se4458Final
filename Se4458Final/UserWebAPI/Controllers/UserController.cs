using AutoMapper;
using Azure;
using BloodBank.Data.Models;
using BloodBank.Logic.Logics.Hospitals;
using Donor.Data.Models;
using Donor.Data.Models.dto.Branch.Dto;
using Donor.Logic.Logics.Branches;
using Location.Data.Models;
using Location.Logic.Logics.Cities;
using Location.Logic.Logics.Towns;
using Microsoft.AspNetCore.Mvc;
using User.Data.Models.dto;
using User.Data.Models.dto.RegisterDto;
using User.Data.Models.dto.UserAndBranch;
using User.Logic.Logics.Users;
using UserWebAPI.Services.Cipher;
using UserWebAPI.Services.Jwt;
using UserWebAPI.Services.Users;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;


namespace UserWebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserLogic _userLogic;
        private readonly IJwtService _jwtService;
        private readonly ICipherService _cipherService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IBranchLogic _branchLogic;
        private readonly IHospitalLogic _hospitalLogic;
        private readonly ICityLogic _cityLogic;
        private readonly ITownLogic _townLogic;

        public UserController(IUserLogic userLogic, IMapper mapper, ICipherService cipherService, IJwtService jwtService, IConfiguration configuration,
            IUserService userService, IBranchLogic branchLogic, IHospitalLogic hospitalLogic, ICityLogic cityLogic, ITownLogic townLogic)
        {
            _userLogic = userLogic;
            _mapper = mapper;
            _jwtService = jwtService;
            _cipherService = cipherService;
            _configuration = configuration;
            _userService = userService;
            _branchLogic = branchLogic;
            _hospitalLogic = hospitalLogic;
            _cityLogic = cityLogic;
            _townLogic = townLogic;
        }
        [HttpPost]
        public async Task<ActionResult<User.Data.Response<UserAndBranchDto>>> RegisterToBranch([FromBody] BranchRegisterDto branchRegisterDto)
        {
            try
            {
                User.Data.Models.User user = _mapper.Map<User.Data.Models.User>(branchRegisterDto);
                bool isUserExist = _userService.CheckUserExistByEmail(user.Email);
                if (isUserExist)
                {
                    return Ok(new BloodBank.Data.Response<UserAndBranchDto> { Message = "USER ALREADY EXİST", Data = new UserAndBranchDto(), Progress = false });
                }
                City? city = _cityLogic.GetSingleByMethod(branchRegisterDto.City);
                if (city == null)
                {
                    return Ok(new BloodBank.Data.Response<UserAndBranchDto> { Message = "City not found", Data = new UserAndBranchDto(), Progress = false });
                }
                Town? town = _townLogic.GetSingleByMethod(branchRegisterDto.Town);
                if (town == null)
                {
                    return Ok(new BloodBank.Data.Response<UserAndBranchDto> { Message = "Town not found", Data = new UserAndBranchDto(), Progress = false });
                }
                Branch? branch = _branchLogic.GetSingleByMethods(city.CityID, town.TownID);
                if (branch == null)
                {
                    return Ok(new BloodBank.Data.Response<UserAndBranchDto> { Message = "Branch not found", Data = new UserAndBranchDto(), Progress = false });
                }
                user.BloodCenterID = branch.BranchID;
                
                _cipherService.CreatePasswordHash(branchRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                
                int userId = _userLogic.AddAndGetId(user);
                
                user.Token = _jwtService.CreateToken(userId, user.Name,"branchemployee", _configuration);
                
                return Ok(await _userService.UpdateUserAndCompleteRegisterOrDeleteIfFailedToBranch(userId, user, branch));              
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<BloodBank.Data.Response<UserAndHospitalDto>>> RegisterToHospital([FromBody] HospitalRegisterDto hospitalRegisterDto)
        {
            try
            {
                User.Data.Models.User user = _mapper.Map<User.Data.Models.User>(hospitalRegisterDto);

                bool isUserExist = _userService.CheckUserExistByEmail(user.Email);
                if (isUserExist)
                {
                    return Ok(new BloodBank.Data.Response<UserAndHospitalDto> { Message = "User already exist", Data = new UserAndHospitalDto(), Progress = false });
                }
                Hospital? hospital = _hospitalLogic.GetSingleByMethodName(hospitalRegisterDto.HospitalName);
                if (hospital == null)
                {
                    return Ok(new BloodBank.Data.Response<UserAndHospitalDto> { Message = "Hospital not found", Data = new UserAndHospitalDto(), Progress = false });
                }
                user.BloodCenterID = hospital.HospitalID;
                _cipherService.CreatePasswordHash(hospitalRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                //Adding to database
                int userId = _userLogic.AddAndGetId(user);

                //Assing token according to branch user or hospital user
                user.Token = _jwtService.CreateToken(userId, user.Name, "hospitalemployee",_configuration);

                //Result
                return Ok(await _userService.UpdateUserAndCompleteRegisterOrDeleteIfFailedToHospital(userId, user, hospital));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            }
        [HttpPost]
        public ActionResult<BloodBank.Data.Response<UserAndBranchLocDto>> LoginToBranch([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                //Check email
                User.Data.Models.User? user = _userLogic.GetSingleByMethod(userLoginDto.Email);
                if (user == null)
                {
                    return Ok(new BloodBank.Data.Response<UserAndBranchLocDto> { Message = "UserNotFound", Data = new UserAndBranchLocDto(), Progress = false });
                }

                //Verify password
                bool isVerified = _cipherService.VerifyPasswordHash(user.PasswordHash, user.PasswordSalt, userLoginDto.Password);
                if (!isVerified)
                {
                    return Ok(new BloodBank.Data.Response<UserAndBranchLocDto> { Message = "Not Found", Data = new UserAndBranchLocDto(), Progress = false });
                }

                Branch? branch = _branchLogic.GetSingle(user.BloodCenterID);
                if(branch == null)
                {
                    return Ok(new BloodBank.Data.Response<UserAndBranchLocDto> { Message = "Branch Not Found", Data = new UserAndBranchLocDto(), Progress = false });
                }

                BranchDto branchDto = new BranchDto()
                {
                    BranchID = branch.BranchID,
                    City = _cityLogic.GetSingle(branch.City).Name,
                    Town = _townLogic.GetSingle(branch.Town).Name,
                    AbMinusBlood = branch.AbMinusBlood,
                    AbPlusBlood = branch.AbPlusBlood,
                    APlusBlood = branch.APlusBlood,
                    AMinusBlood = branch.AMinusBlood,
                    BMinusBlood= branch.BMinusBlood,
                    BPlusBlood= branch.BPlusBlood,
                    ZeroMinusBlood = branch.ZeroMinusBlood,
                    ZeroPlusBlood = branch.ZeroPlusBlood,
                    GeopointID = branch.GeopointID,
                };
                //Result
                return Ok(new BloodBank.Data.Response<UserAndBranchLocDto> { Message = "Login successfully", Data = new UserAndBranchLocDto(){ User =user, Branch = branchDto }, Progress = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public ActionResult<BloodBank.Data.Response<UserAndHospitalDto>> LoginToHospital([FromBody] UserLoginDto userLoginDto)
        {
            try
            {
                //Check email
                User.Data.Models.User? user = _userLogic.GetSingleByMethod(userLoginDto.Email);
                if (user == null)
                {
                    return Ok(new BloodBank.Data.Response<UserAndHospitalDto> { Message = "User Not Found", Data = new UserAndHospitalDto(), Progress = false });
                }

                //Verify password
                bool isVerified = _cipherService.VerifyPasswordHash(user.PasswordHash, user.PasswordSalt, userLoginDto.Password);
                if (!isVerified)
                {
                    return Ok(new BloodBank.Data.Response<UserAndHospitalDto> { Message = "User Not Found", Data = new UserAndHospitalDto(), Progress = false });
                }

                Hospital? hospital = _hospitalLogic.GetSingle(user.BloodCenterID);
                if (hospital == null)
                {
                    return Ok(new BloodBank.Data.Response<UserAndHospitalDto> { Message = "Hospital Not Found", Data = new UserAndHospitalDto(), Progress = false });
                }


                //Result
                return Ok(new BloodBank.Data.Response<UserAndHospitalDto> { Message = "User Login Successfully", Data = new UserAndHospitalDto() { User = user, Hospital = hospital }, Progress = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}