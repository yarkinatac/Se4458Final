using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Drawing;
using System.Numerics;
using BloodBank.Data.Models.dto.Hospital.Dto;
using BloodBank.Data;
using BloodBank.Data.Models;
using BloodBank.Data.Models;
using BloodBank.Data.Models.dto.Hospital.Dto;

namespace BloodBankWebAPI.Services.Mapper
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<HospitalDto, Hospital>();
        }
    }
}
