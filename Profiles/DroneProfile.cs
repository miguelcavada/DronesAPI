using AutoMapper;
using DronesAPI.Dtos;
using DronesAPI.Models;

namespace DronesAPI.Profiles
{
    public class DroneProfile : Profile
    {
        public DroneProfile()
        {
            CreateMap<Drone, DroneDto>();
            CreateMap<DroneForCreationDto, Drone>();
        }
    }
}
