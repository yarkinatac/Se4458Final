using AutoMapper;
using System.Drawing;
using User.Data.Models;
using User.Data.Models.dto;
using User.Data.Models.dto.RegisterDto;
using UserWebAPI.Services.Users;

namespace UserWebAPI.Services.Mapper
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<BranchRegisterDto, User.Data.Models.User>();
            CreateMap<HospitalRegisterDto, User.Data.Models.User>();

        }
    }
}
