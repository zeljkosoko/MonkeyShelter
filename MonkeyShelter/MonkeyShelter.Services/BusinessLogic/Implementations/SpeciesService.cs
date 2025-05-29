using AutoMapper;
using Microsoft.Extensions.Logging;
using MonkeyShelter.Core.Domain_exceptions;
using MonkeyShelter.Core.DTOs;
using MonkeyShelter.Core.Entities;
using MonkeyShelter.Core.Interfaces;
using MonkeyShelter.Services.BusinessLogic.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Services.BusinessLogic.Implementations
{
    public class SpeciesService: ISpeciesService
    {
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SpeciesService> _logger;
        public SpeciesService(ISpeciesRepository speciesRepository, IMapper mapper, ILogger<SpeciesService> logger)
        {
            _speciesRepository = speciesRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SpeciesDto>> GetAllSpeciesAsync()
        {
            var species = await _speciesRepository.GetAllAsync();
            var speciesDto = _mapper.Map<IEnumerable<SpeciesDto>>(species);
            return speciesDto;
        }

        public async Task<SpeciesDto?> GetSpeciesByAsync(int id)
        {
            var species = await _speciesRepository.GetByIdAsync(id);
            return species != null ? _mapper.Map<SpeciesDto>(species) : null;
        }

        public async Task CreateAsync(CreateSpeciesDto speciesDto)
        {
            if (await _speciesRepository.ExistsSpecies(speciesDto.Name))
            {
                _logger.LogWarning($"There is already exist species {speciesDto.Name}");
                throw new SpeciesAlreadyExistsException($"There is already exist species {speciesDto.Name}");
            }

            await _speciesRepository.AddAsync(_mapper.Map<Species>(speciesDto));
            _logger.LogInformation($"{speciesDto.Name} save in DB successfully.");
        }
    }
}
