using AutoMapper;
using Donor.Logic.Logics.Donors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using User.Data;
using Donor.Data.Models;
using Donor.Data.Models.dto.Donor.dto;
using Donor.Logic.Logics.Branches;
using Donor.Data.Models.dto.DonationHistory.Dto;
using Donor.Logic.Logics.DonationHistories;
using DonorWebAPI.Services.Donors;
using Donor.Logic.Logics.JoinTable;
using DonorWebAPI.Services.Jwt;
using User.Logic.Logics.Users;
using DonorWebAPI.Services.Security;
using DonorWebAPI.Services.Location;
using DonorWebAPI.Services.Blob;
using Location.Logic.Logics.Cities;
using Location.Logic.Logics.Towns;
using BloodBank.Data.Models;
using Donor.Data.Models.dto.Donor.Dto;

namespace DonorWebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1")]
    
    public class DonorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IDonorLogic _donorLogic;
        private readonly IBranchLogic _branchLogic;
        private readonly ISecurityService _securityService;
        private readonly IDonationHistoryLogic _donationHistoryLogic;
        private readonly ILocationService _locationService;
        private readonly IDonorService _donorService;
        private readonly IJoinTable _joinTable;
        private readonly IJwtService _jwtService;
        private readonly IUserLogic _userLogic;
        private readonly ICityLogic _cityLogic;
        private readonly ITownLogic _townLogic;


        public DonorController(IMapper mapper, IConfiguration configuration, IDonorLogic donorLogic, ISecurityService securityService, ILocationService locationService, IBranchLogic branchLogic, IDonationHistoryLogic donationHistoryLogic, IDonorService donorService, IJoinTable joinTable, IJwtService jwtService, IUserLogic userLogic, ICityLogic cityLogic, ITownLogic townLogic)

        {
            _mapper = mapper;
            _configuration = configuration;
            _donorLogic = donorLogic;
            _securityService = securityService;
            _locationService = locationService;
            _branchLogic = branchLogic;
            _donationHistoryLogic = donationHistoryLogic;
            _donorService = donorService;
            _joinTable = joinTable;
            _jwtService = jwtService;
            _userLogic = userLogic;
            _cityLogic = cityLogic;
            _townLogic = townLogic;
        }
        [HttpPost,Authorize(Roles ="branchemployee")]
        public async Task<ActionResult<Response<Donor.Data.Models.Donor>>> Add([FromForm] DonorAdditionDto donorAdditionDto)
        {
            try
            {
                if (_securityService.Verify(Request.Headers))
                {
                   
                    bool isBranchExist = _branchLogic.CheckExistence(donorAdditionDto.BranchID);
                    if (!isBranchExist)
                    {
                        return Ok(new Response<Donor.Data.Models.Donor> { Message = "BranchNotFound", Data = new Donor.Data.Models.Donor(), Progress = false });
                    }
                    int? townId = _locationService.GetTownIfNotExistCreateAndGet(donorAdditionDto.Town);
                    if (townId == null)
                    {
                        return Ok(new Response<Donor.Data.Models.Donor> { Message = "Town not added", Data = new Donor.Data.Models.Donor(), Progress = false });
                    }
                    int? cityId = _locationService.GetCityIfNotExistCreateAndGet(donorAdditionDto.City);
                    if(cityId == null)
                    {
                        return Ok(new Response<Donor.Data.Models.Donor> { Message = "City Not Added", Data = new Donor.Data.Models.Donor(), Progress = false });
                    }
                    
                    Donor.Data.Models.Donor donor = new Donor.Data.Models.Donor
                    {
                        BranchID = donorAdditionDto.BranchID,
                        Name = donorAdditionDto.Name.ToLower(),
                        Surname = donorAdditionDto.Surname.ToLower(),
                        BloodType = donorAdditionDto.BloodType,
                        PhoneNumber = donorAdditionDto.PhoneNumber,
                        City = (int)cityId,
                        Town = (int)townId,

                    };
                    int isDonorAdded = _donorLogic.AddAndGetIdDonor(donor);
                    if (!(isDonorAdded > 0)) 
                    {
                        return Ok(new Response<Donor.Data.Models.Donor> { Message = "Donor not added", Data = new Donor.Data.Models.Donor(), Progress = false });
                    }
                    BlobService _blobService = new BlobService();
                    await _blobService.UploadPhotoAsync(donorAdditionDto.DonorPhoto);
                  

                    return Ok(new Response<Donor.Data.Models.Donor> { Message = "donor added", Data = donor, Progress = true });
                }
                else
                {
                    return BadRequest(new Response<Donor.Data.Models.Donor> { Message = "user not verify", Data = new Donor.Data.Models.Donor(), Progress = false });
                }
            }


            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
        
        [HttpPost,Authorize(Roles ="branchemployee")]
        public ActionResult<Response<Donor.Data.Models.Donor>> Find([FromBody] IdentificationDto donorIdentificationDto)
        {
            try
            {
                if (_securityService.Verify(Request.Headers))
                {
                    int userId = _jwtService.GetUserIdFromToken(Request.Headers);
                    User.Data.Models.User? user = _userLogic.GetSingleByMethod(userId);
                    if (user == null)
                    {
                        return Ok(new Response<Donor.Data.Models.Donor> { Message = "uSER nOT FOUIND", Data = new Donor.Data.Models.Donor(), Progress = false });
                    }
                    List<Donor.Data.Models.Donor>? donorList = _joinTable.FindDonorByJoinTable(user.BloodCenterID,donorIdentificationDto.Name.ToLower(), donorIdentificationDto.Surname.ToLower());
                    if(donorList.Count == 0)
                    {
                        return Ok(new Response<Donor.Data.Models.Donor> { Message = "Donor not found", Data = new Donor.Data.Models.Donor(), Progress = false });
                    }
                    return Ok(new Response<Donor.Data.Models.Donor> { Message = "donor successfull found", Data = donorList[0], Progress = true });
                }
                else
                {
                    return BadRequest(new Response<Donor.Data.Models.Donor> { Message = "user not verify", Data = new Donor.Data.Models.Donor(), Progress = false });
                }
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost,Authorize(Roles ="branchemployee")]
        public async Task<ActionResult<Response<DonationHistory>>> Donate([FromBody] DonationHistoryDto donationHistoryDto)
        {
            try
            {
                if (_securityService.Verify(Request.Headers))
                {
                    Donor.Data.Models.Donor? donor = _donorLogic.GetSingleByMethod(donationHistoryDto.DonorID);
                    if (donor == null)
                    {
                        return Ok(new Response<DonationHistory> { Message = "not found", Data = new DonationHistory(), Progress = false });
                    }
                    Branch? branch = _branchLogic.GetSingleByMethod(donor.BranchID);
                    if (branch == null)
                    {
                        return Ok(new Response<DonationHistory> { Message = "branch not found", Data = new DonationHistory(), Progress = false });
                    }
                    bool isBranchBloodCountUpdated = await _donorService.UpdateBranchBloodNumber(branch,donor.BloodType,donationHistoryDto.TupleNumber);
                    if (!isBranchBloodCountUpdated)
                    {
                        return Ok(new Response<DonationHistory> { Message = "", Data = new DonationHistory(), Progress = false });
                    }

                    
                    DonationHistory donationHistory = new DonationHistory()
                    {
                        DonationDate = DateTime.Now,
                        DonorID = donationHistoryDto.DonorID,
                        TupleNumber = donationHistoryDto.TupleNumber,
                    };

                    bool isDonationHistoryAdded = _donationHistoryLogic.Add(donationHistory);
                    if (!isDonationHistoryAdded)
                    {
                        bool isUndoBranchBloodCountUpdated = await _donorService.UndoUpdateBranchBloodNumber(branch, donor.BloodType, donationHistoryDto.TupleNumber);
                        if (!isUndoBranchBloodCountUpdated)
                        {
                            return Ok(new Response<DonationHistory> { Message = "", Data = new DonationHistory(), Progress = false });
                        }
                        return Ok(new Response<DonationHistory> { Message = "", Data = new DonationHistory(), Progress = false });
                    }
                    return Ok(new Response<DonationHistory> { Message = "", Data = donationHistory, Progress = true });
                }
                else
                {
                    return BadRequest(new Response<Donor.Data.Models.Donor> { Message = "", Data = new Donor.Data.Models.Donor(), Progress = false });
                }
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost,Authorize(Roles ="branchemployee")]
        public ActionResult<Response<List<DonorListDto>>> GetList()
        {
            try
            {
                if (_securityService.Verify(Request.Headers))
                {
                    int userId = _jwtService.GetUserIdFromToken(Request.Headers);
                    User.Data.Models.User? user = _userLogic.GetSingle(userId);
                    if (user == null)
                    {
                        return Ok(new Response<Hospital> { Message = "", Data = new Hospital(), Progress = false });
                    }
                    Branch? branch = _branchLogic.GetSingle(user.BloodCenterID);
                    if (branch == null)
                    {
                        return BadRequest(new Response<List<Donor.Data.Models.Donor>> { Message = "", Data = new List<Donor.Data.Models.Donor>(), Progress = false });
                    }
                    
                    var list = _joinTable.GetDonorListByJoinTable(user.BloodCenterID);
                    List<DonorListDto> donorList = new List<DonorListDto>();
                    if (list.Count != 0)
                    {
                       
                        foreach(var item in list)
                        {
                            DonorListDto donorListDto = new DonorListDto()
                            {
                                DonorID = item.DonorID,
                                Name = item.Name,
                                Surname = item.Surname,
                                BloodType = item.BloodType,
                                BranchID = item.BranchID,
                                PhoneNumber = item.PhoneNumber,
                                City = _cityLogic.GetSingle(item.City).Name,
                                Town = _townLogic.GetSingle(item.Town).Name,


                            };
                            donorList.Add(donorListDto);
                        }
                        return Ok(new Response<List<DonorListDto>> { Message = "", Data = donorList, Progress = true });
                        
                    }
                    return BadRequest(new Response<List<DonorListDto>> { Message = "", Data = new List<DonorListDto>(), Progress = false });
                }
                else
                {
                    return BadRequest(new Response<Donor.Data.Models.Donor> { Message = "", Data = new Donor.Data.Models.Donor(), Progress = false });
                }
            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
