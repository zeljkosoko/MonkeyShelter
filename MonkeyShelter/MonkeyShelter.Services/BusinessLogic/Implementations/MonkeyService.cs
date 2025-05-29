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
    //*** It uses only custom exceptions for domain errors **
    public class MonkeyService : IMonkeyService
    {
        private readonly IMonkeyRepository _monkeyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MonkeyService> _logger;
        public MonkeyService(
            IMonkeyRepository monkeyRepository,
            IMapper mapper,
            ILogger<MonkeyService> logger)
        {
            _monkeyRepository = monkeyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MonkeyDto>> GetAllAsync()
        {
            var monkeys = await _monkeyRepository.GetAllAsync();
            var monkeyDtos = _mapper.Map<IEnumerable<MonkeyDto>>(monkeys);

            return monkeyDtos;
        }

        public async Task<MonkeyDto?> GetMonkeyAsync(Guid id)
        {
            //There's no exceptions.-> Monkey exists or it is null
            var monkey = await _monkeyRepository.GetByIdAsync(id);

            return monkey == null ? null : _mapper.Map<MonkeyDto>(monkey);
        }

        public async Task UpdateWeightAsync(Guid id, double neWeight)
        {
            var monkey = await GetExistingMonkeyOrThrow(id);

            monkey.Weight = neWeight;
            await _monkeyRepository.UpdateAsync(monkey);

            _logger.LogInformation($"Monkey {id} updated successfully.");
        }

        public async Task DeleteMonkeyAsync(Guid id)
        {
            var monkey = await GetExistingMonkeyOrThrow(id);

            await _monkeyRepository.RemoveAsync(id);

            _logger.LogInformation("Monkey is successfully deleted for ID: {MonkeyId}", id);
        }


        public async Task<MonkeyDto> RegisterMonkeyArrivalAsync(CreateMonkeyDto createMonkeyDto)
        {
            var now = DateTime.UtcNow;

            if (!await _monkeyRepository.CanMonkeyArriveAsync(now))
            {
                _logger.LogWarning("There is 7 arrivals today. Limit is reached!");
                //Custom specific exception for domain error:
                throw new MaxArrivalCountReachedExc("Arrivals limit for today has been reached!");
            }

            var monkey = _mapper.Map<Monkey>(createMonkeyDto);
            monkey.ArrivalDate = now;

            await _monkeyRepository.AddAsync(monkey);
            _logger.LogInformation("Monkey with {MonkeyId} has been registered in shelter.", monkey.Id);

            return _mapper.Map<MonkeyDto>(monkey);
        }

        public async Task<MonkeyDto> RegisterMonkeyDepartureAsync(Guid id)
        {
            var now = DateTime.UtcNow;

            var monkey = await GetExistingMonkeyOrThrow(id);

            //Check the restrictions
            if (monkey.DepartureDate.HasValue)
            {
                _logger.LogWarning("Monkey ID {MonkeyId} has already been leave the shelter", id);
                throw new BusinessRuleException($"Monkey Id {id} has already been leave the shelter");
            }
            if(! await _monkeyRepository.CanMonkeyLeaveAsync(now, monkey.SpeciesId))
            {
                _logger.LogWarning("Departure denied.Either species count is to low or departure-arrival is less than 1.");
                throw new DepartureNotAllowedException("Departure denied.Either species count is to low or departure-arrival is less than 1.");
            }

            monkey.DepartureDate = now;
            await _monkeyRepository.UpdateAsync(monkey);
            _logger.LogInformation("Monkey ID {MonkeyId} is departed from shelter", id);

            return _mapper.Map<MonkeyDto>(monkey);
        }

        private async Task<Monkey> GetExistingMonkeyOrThrow(Guid id)
        {
            var monkey = await _monkeyRepository.GetByIdAsync(id);
            if(monkey == null)
            {
                _logger.LogWarning("Monkey {MonkeyId} is not found", id);
                
                    //Custom specific exception for domain error:
                throw new CustomNotFoundException($"Monkey {id} not found.");
            }

            return monkey;
        }

        #region Old PostMonkeyAsync
        public async Task PostMonkeyAsync(CreateMonkeyDto createMonkeyDto)
        {
            var monkey = _mapper.Map<Monkey>(createMonkeyDto);

            await _monkeyRepository.AddAsync(monkey);
            _logger.LogInformation("Monkey {MonkeyId} added successfully!", monkey.Id);
        }
        #endregion
    }
}
