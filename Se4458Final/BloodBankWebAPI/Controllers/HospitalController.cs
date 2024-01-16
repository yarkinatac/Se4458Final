using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

using AutoMapper;
using BloodBank.Logic.Logics.Hospitals;
using BloodBank.Data.Models.dto.Hospital.Dto;
using BloodBank.Data;
using BloodBank.Data.Models;

namespace BloodBankWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HospitalController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IHospitalLogic _hospitalLogic;


        public HospitalController(IHospitalLogic hospitalLogic, IMapper mapper)
        {
            _hospitalLogic = hospitalLogic;
            _mapper = mapper;


        }
        
        [HttpPost]
        public ActionResult<int> AddHospital([FromBody] HospitalDto hospitalDto)
        {
            try
            {
                Hospital hospital = _mapper.Map<Hospital>(hospitalDto);
                int companyId = _hospitalLogic.AddAndGetIdHospital(hospital);
                if (companyId != -1)
                {
                    return Ok(hospital);
                }
                return Ok("no");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
