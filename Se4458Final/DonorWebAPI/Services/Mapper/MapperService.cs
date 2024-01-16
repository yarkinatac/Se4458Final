using AutoMapper;


using Donor.Data.Models.dto.Donor.Dto;
using Donor.Data.Models;
using Donor.Data.Models.dto.DonationHistory.Dto;

namespace DonorWebAPI.Services.Mapper
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<DonorAdditionDto, Donor.Data.Models.Donor>();
            CreateMap<DonationHistoryDto, DonationHistory>();

        }
    }
}
