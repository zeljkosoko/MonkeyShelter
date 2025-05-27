using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MonkeyShelter.Core.DTOs;
using MonkeyShelter.Core.Entities;

namespace MonkeyShelter.Services.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            //Mapping for Monkey
            CreateMap<Monkey, MonkeyDto>()
                .ForMember(dest => dest.SpeciesName, opt => opt.MapFrom(src => src.Species!.Name))
                .ForMember(dest => dest.ShelterName, opt => opt.MapFrom(src => src.Shelter!.Name));

            CreateMap<CreateMonkeyDto, Monkey>()
                .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            //Mapping for Species
            CreateMap<Species, SpeciesDto>().ReverseMap();
            CreateMap<CreateSpeciesDto, Species>();

            //Mapping for Shelter
            CreateMap<Shelter, ShelterDto>().ReverseMap();
            CreateMap<CreateShelterDto, Shelter>();
            
            //Mapping for VetCheck
            CreateMap<VetCheck, VetCheckDto>();
            CreateMap<CreateVetCheckDto, VetCheck>();

        }
    }
}
