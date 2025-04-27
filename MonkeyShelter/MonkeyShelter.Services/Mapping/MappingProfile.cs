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
            CreateMap<Monkey, MonkeyDto>()
                .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Species!.Name))
                .ForMember(dest => dest.Shelter, opt => opt.MapFrom(src => src.Shelter!.Name));

            CreateMap<CreateMonkeyDto, Monkey>()
                .ForMember(dest => dest.ArrivalDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
