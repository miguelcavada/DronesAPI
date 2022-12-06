using AutoMapper;
using DronesAPI.Dtos;
using DronesAPI.Models;

namespace DronesAPI.Profiles
{
    public class MedicationProfile: Profile
    {
        public MedicationProfile()
        {
            CreateMap<Medication, MedicationDto>();
        }
    }
}
