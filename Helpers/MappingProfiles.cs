using AutoMapper;
using FleetPulse_BackEndDevelopment.Data.DTO;
using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LoginDTO, User>().ReverseMap();
    }
}