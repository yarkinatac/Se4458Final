using BloodBank.Data.Models.dto.RequestBlood.Dto;



using Donor.Data.Models;
using Donor.Logic.Logics.Branches;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;


using Location.Data.Models;

using Location.Logic.Logics.Cities;
using Location.Logic.Logics.Towns;

using BloodBankAPI.Services.Jwt;
using User.Logic.Logics.Users;

using BloodBank.Data.Models;
using BloodBank.Logic.Logics.Hospitals;

using BloodBank.Logic.Logics.BloodRequests;


using BloodBank.Logic.Logics.JoinTable;
using Microsoft.IdentityModel.Tokens;
using Location.Logic.Logics.Geopoints;
using BloodBank.Logic;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using Azure;
using BloodBankAPI;
using BloodBankWebAPI.Services.Donors;
using BloodBankWebAPI.Services.Location;
using BloodBankWebAPI.Services.Mail;
using BloodBankWebAPI.Services.Security;


namespace BloodBankWebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1")]
    public class BloodRequestController : Controller
    {
        private readonly IBranchLogic _branchLogic;
        private readonly ISecurityService _securityService;
        private readonly IDonorService _donorService;
        private readonly ILocationService _locationService;
        private readonly ICityLogic _cityLogic;
        private readonly ITownLogic _townLogic;
        private readonly IJoinTable _joinTable;
        private readonly IJwtService _jwtService;
        private readonly IUserLogic _userLogic;
        private readonly IHospitalLogic _hospitalLogic;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private readonly IBloodRequestLogic _bloodRequestLogic;
        private readonly IGeopointLogic _geopointLogic;


        public BloodRequestController(IBranchLogic branchLogic, ISecurityService securityService, ILocationService locationService, ICityLogic cityLogic, ITownLogic townLogic, IDonorService donorService, IJwtService jwtService, IUserLogic userLogic, IHospitalLogic hospitalLogic, IMailService mailService, IConfiguration configuration, IBloodRequestLogic bloodRequestLogic, IJoinTable joinTable, IGeopointLogic geopointLogic)
        {
            _branchLogic = branchLogic;
            _securityService = securityService;
            _locationService = locationService;
            _cityLogic = cityLogic;
            _townLogic = townLogic;
            _donorService = donorService;
            _jwtService = jwtService;
            _userLogic = userLogic;
            _hospitalLogic = hospitalLogic;
            _mailService = mailService;
            _configuration = configuration;
            _bloodRequestLogic = bloodRequestLogic;
            _joinTable = joinTable;
            _geopointLogic = geopointLogic;
        }
       [HttpPost, Authorize(Roles = "hospitalemployee")]
public async Task<ActionResult<Hospital>> RequestBlood([FromBody] BloodRequestAndBranchDto bloodRequestAndBranchDto)
{
    try
    {
        if (!_securityService.Verify(Request.Headers))
        {
            return BadRequest(new User.Data.Response<Hospital> { Message = "User is not verified", Data = new Hospital(), Progress = false });
        }

        int userId = _jwtService.GetUserIdFromToken(Request.Headers);
        User.Data.Models.User? user = _userLogic.GetSingle(userId);

        if (user == null)
        {
            return NotFound(new User.Data.Response<Hospital> { Message = "User Not Found", Data = new Hospital(), Progress = false });
        }

        Hospital? hospital = _hospitalLogic.GetSingle(user.BloodCenterID);

        if (hospital == null)
        {
            return NotFound(new User.Data.Response<Hospital> { Message = "Hospital Not Found", Data = new Hospital(), Progress = false });
        }

        City? city = _cityLogic.GetSingleByMethod(bloodRequestAndBranchDto.City);

        if (city == null)
        {
            return NotFound(new User.Data.Response<Hospital> { Message = "City Not Found", Data = new Hospital(), Progress = false });
        }

        Town? town = _townLogic.GetSingleByMethod(bloodRequestAndBranchDto.Town);

        if (town == null)
        {
            return NotFound(new User.Data.Response<Hospital> { Message = "Town Not Found", Data = new Hospital(), Progress = false });
        }

        Branch? branch = _branchLogic.GetSingleByMethods(city.CityID, town.TownID);

        if (branch == null)
        {
            return NotFound(new User.Data.Response<Hospital> { Message = "Branch Not Found", Data = new Hospital(), Progress = false });
        }

        if (_donorService.HasBlood(branch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodNumber))
        {
            bool isBranchBloodCountDecreased = await _donorService.UndoUpdateBranchBloodNumber(branch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodNumber);

            if (!isBranchBloodCountDecreased)
            {
                return Ok(new User.Data.Response<Hospital> { Message = "Not Decreased", Data = new Hospital(), Progress = false });
            }

            bool isHospitalBloodCountIncreased = await _donorService.UpdateHospitalBloodNumber(hospital, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodNumber);

            if (!isHospitalBloodCountIncreased)
            {
                bool isBranchBloodCountIncreased = await _donorService.UpdateBranchBloodNumber(branch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodNumber);

                if (!isBranchBloodCountIncreased)
                {
                    return Ok(new User.Data.Response<Hospital> { Message = "Branch Blood Count Not Increased", Data = new Hospital(), Progress = false });
                }

                return Ok(new User.Data.Response<Hospital> { Message = "Blood Number not increased", Data = new Hospital(), Progress = false });
            }

            _mailService.SendMail(bloodRequestAndBranchDto.Email, "Blood request", $"Your request {bloodRequestAndBranchDto.BloodType} {bloodRequestAndBranchDto.BloodNumber} unit is successfully complete", _configuration);

            return Ok(new User.Data.Response<Hospital> { Message = "Transfer Successful", Data = hospital, Progress = true });
        }
        else
        {
            Geopoint geopoint = _geopointLogic.GetSingle(bloodRequestAndBranchDto.GeopointID);
            RequestedGeopointDto requestedGeopointDto = new RequestedGeopointDto();
            GeopointDto hospitalGeopoint = new GeopointDto()
            {
                Latitude = geopoint.Latitude,
                Longitude = geopoint.Longitude,
            };

            requestedGeopointDto.HospitalGeopoint = hospitalGeopoint;
            List<IdDto> idDto = _joinTable.AllBranchListByJoinTable();
            List<GeopointDto> geopointDtoList = new List<GeopointDto>();

            foreach (IdDto idDtoItem in idDto)
            {
                Geopoint geo = _geopointLogic.GetSingle(idDtoItem.Id);
                geopointDtoList.Add(new GeopointDto() { Latitude = geo.Latitude, Longitude = geo.Longitude });
            }

            requestedGeopointDto.RequestedBloodLine = geopointDtoList;
            List<GeopointDto> nearestBranchList = DistanceManager.FindNearBranch(requestedGeopointDto);

            if (nearestBranchList.IsNullOrEmpty())
            {
                return Ok(new User.Data.Response<Hospital> { Message = "Not Decreased", Data = new Hospital(), Progress = false });
            }

            bool isFlag = false;

            for (int i = 0; i < nearestBranchList.Count; i++)
            {
                Geopoint? geoPoint = _geopointLogic.GetSingleByMethods(nearestBranchList[i].Latitude, nearestBranchList[i].Longitude);
                Branch? nearestBranch = _branchLogic.GetSingleByMethodGeoPoint(geoPoint.GeopointID);

                if (_donorService.HasBlood(nearestBranch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodNumber))
                {
                    bool isBranchBloodCountDecreased = await _donorService.UndoUpdateBranchBloodNumber(nearestBranch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodNumber);

                    if (!isBranchBloodCountDecreased)
                    {
                        return Ok(new User.Data.Response<Hospital> { Message = "Not Decreased", Data = new Hospital(), Progress = false });
                    }

                    bool isHospitalBloodCountIncreased = await _donorService.UpdateHospitalBloodNumber(hospital, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodNumber);

                    if (!isHospitalBloodCountIncreased)
                    {
                        bool isBranchBloodCountIncreased = await _donorService.UpdateBranchBloodNumber(branch, bloodRequestAndBranchDto.BloodType, bloodRequestAndBranchDto.BloodNumber);

                        if (!isBranchBloodCountIncreased)
                        {
                            return Ok(new User.Data.Response<Hospital> { Message = "Blood Number not increased", Data = new Hospital(), Progress = false });
                        }

                        return Ok(new User.Data.Response<Hospital> { Message = "Blood Number not increased", Data = new Hospital(), Progress = false });
                    }

                    _mailService.SendMail(bloodRequestAndBranchDto.Email, "Blood request", $"Your request {bloodRequestAndBranchDto.BloodType} {bloodRequestAndBranchDto.BloodNumber} unit is successfully complete", _configuration);
                    isFlag = true;

                    return Ok(new User.Data.Response<Hospital> { Message = "Success", Data = hospital, Progress = true });
                }
            }

            if (!isFlag)
            {
                RequestForBlood requestBlood = new RequestForBlood()
                {
                    BloodNumber = bloodRequestAndBranchDto.BloodNumber,
                    BloodType = bloodRequestAndBranchDto.BloodType,
                    DurationDate = bloodRequestAndBranchDto.DurationDate,
                    Town = bloodRequestAndBranchDto.Town,
                    City = bloodRequestAndBranchDto.City,
                    HospitalLongitude = geopoint.Longitude,
                    HospitalLatitude = geopoint.Latitude,
                    HospitalEmail = bloodRequestAndBranchDto.Email,
                };

                bool isBloodRequestAdded = _bloodRequestLogic.AddReq(requestBlood);

                if (!isBloodRequestAdded)
                {
                    return Ok(new User.Data.Response<RequestForBlood> { Message = "Not Added", Data = new RequestForBlood(), Progress = false });
                }

                return Ok(new User.Data.Response<RequestForBlood> { Message = "Successfully Added", Data = requestBlood, Progress = true });
            }
        }

        return Ok(new User.Data.Response<Hospital> { Message = "Unknown Error", Data = new Hospital(), Progress = false });
    }
    catch (Exception ex)
    {
        // Hata mesajını loglama veya işlemeniz gereken diğer adımları burada ekleyin
        Console.WriteLine($"Hata: {ex.Message}");
        return StatusCode(500, new User.Data.Response<Hospital> { Message = "Internal Server Error", Data = new Hospital(), Progress = false });
    }
}

        
        [HttpPost]
        public async Task<ActionResult> FindDistance()
        {
            RequestedGeopointDto requestedGeopointDto = new RequestedGeopointDto();
            requestedGeopointDto.HospitalGeopoint = new GeopointDto()
            {
                Latitude = 40.469939,
                Longitude = 37.210308,
            };
            List<GeopointDto> list = new List<GeopointDto>();
            list.Add(new GeopointDto()
            {
                Latitude = 28.310211,
                Longitude = 36.316068,

            });

            requestedGeopointDto.RequestedBloodLine = list;


            return Ok(DistanceManager.FindNearBranch(requestedGeopointDto));
        }
        [HttpPost]
        public async Task<ActionResult> AutomaticRequestBloodWithAzureLogicApp()
        {
            List<RequestForBlood> bloodRequestList = _joinTable.CheckBloodRequestByJoinTable();
            foreach (RequestForBlood bloodRequestListItem in bloodRequestList)
            {
                Geopoint geopoint = _geopointLogic.GetSingleByMethods(bloodRequestListItem.HospitalLatitude, bloodRequestListItem.HospitalLongitude);
                Hospital hospital = _hospitalLogic.GetSingleByMethodGeopoint(geopoint.GeopointID);
                City? city = _cityLogic.GetSingleByMethod(bloodRequestListItem.City);
                Town? town = _townLogic.GetSingleByMethod(bloodRequestListItem.Town);
                Branch? branch = _branchLogic.GetSingleByMethods(city.CityID, town.TownID);
                
                if (_donorService.HasBlood(branch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodNumber))
                {
                    bool isBranchBloodCountDecreased = await _donorService.UndoUpdateBranchBloodNumber(branch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodNumber);
                    bool isHospitalBloodCountIncreased = await _donorService.UpdateHospitalBloodNumber(hospital, bloodRequestListItem.BloodType, bloodRequestListItem.BloodNumber);
                    if (!isHospitalBloodCountIncreased)
                    {
                        bool isBranchBloodCountIncreased = await _donorService.UpdateBranchBloodNumber(branch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodNumber);
                    }
                }
                else
                {
                    RequestedGeopointDto requestedGeopointDto = new RequestedGeopointDto();
                    GeopointDto hospitalGeopoint = new GeopointDto()
                    {
                        Latitude = bloodRequestListItem.HospitalLatitude,
                        Longitude = bloodRequestListItem.HospitalLongitude,
                    };
                    requestedGeopointDto.HospitalGeopoint = hospitalGeopoint;
                    List<IdDto> idDto = _joinTable.AllBranchListByJoinTable();
                    List<GeopointDto> geopointDtoList = new List<GeopointDto>();
                    foreach (IdDto idDtoItem in idDto)
                    {
                        Geopoint geo = _geopointLogic.GetSingle(idDtoItem.Id);
                        geopointDtoList.Add(new GeopointDto() { Latitude = geo.Latitude, Longitude = geo.Longitude });
        
                    }
                    requestedGeopointDto.RequestedBloodLine = geopointDtoList;
                    List<GeopointDto> nearestBranchList = DistanceManager.FindNearBranch(requestedGeopointDto);
                    for (int i = 0; i < nearestBranchList.Count; i++)
                    {
                        Geopoint? geoPoint = _geopointLogic.GetSingleByMethods(nearestBranchList[i].Latitude, nearestBranchList[i].Longitude);
                        Branch? nearestBranch = _branchLogic.GetSingleByMethodGeoPoint(geoPoint.GeopointID);
                        if (_donorService.HasBlood(nearestBranch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodNumber))
                        {
                            bool isBranchBloodCountDecreased = await _donorService.UndoUpdateBranchBloodNumber(nearestBranch, bloodRequestListItem.BloodType, bloodRequestListItem.BloodNumber);
                            bool isHospitalBloodCountIncreased = await _donorService.UpdateHospitalBloodNumber(hospital, bloodRequestListItem.BloodType, bloodRequestListItem.BloodNumber);
                            bloodRequestListItem.DurationDate = DateTime.Now.AddDays(-1);
                            await _bloodRequestLogic.UpdateAsync(bloodRequestListItem.RequestID,bloodRequestListItem);
                        }
                    }
                }
            }
            return Ok();
        
        }
    }
}