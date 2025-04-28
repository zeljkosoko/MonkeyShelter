using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonkeyShelter.Core.DTOs;
using MonkeyShelter.Core.Entities;
using MonkeyShelter.Core.Interfaces;
using MonkeyShelter.Services.Reports;

namespace MonkeyShelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")] //Manager is authorized to use the all endpoints
    public class MonkeysController : ControllerBase
    {
        private readonly IMonkeyRepository _monkeyRepository;
        private readonly IMapper _mapper;
        private readonly IReportService _reportService;

        public MonkeysController(
            IMonkeyRepository monkeyRepository, 
            IMapper mapper, 
            IReportService reportService)
        {
            _monkeyRepository = monkeyRepository;
            _mapper = mapper;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonkeyDto>>> GetMonkeys()
        {
            var monkeys = await _monkeyRepository.GetAllAsync();

            return Ok(_mapper.Map<IEnumerable<MonkeyDto>>(monkeys));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MonkeyDto>> GetMonkey(Guid id)
        {
            var monkey = await _monkeyRepository.GetByIdAsync(id);
            if (monkey == null)
                return NotFound();

            return Ok(_mapper.Map<MonkeyDto>(monkey));
        }

        [HttpPost]
        public async Task<ActionResult<MonkeyDto>> PostMonkey(CreateMonkeyDto dto)
        {
            var monkey = _mapper.Map<Monkey>(dto);
            await _monkeyRepository.AddAsync(monkey);
            return CreatedAtAction(nameof(GetMonkey), new { id = monkey.Id }, _mapper.Map<MonkeyDto>(monkey));
        }

        [HttpPut("{id}/weight")]
        public async Task<IActionResult> UpdateWeight(Guid id, [FromBody] double newWeight)
        {
            await _monkeyRepository.UpdateWeightAsync(id, newWeight);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonkey(Guid id)
        {
            await _monkeyRepository.RemoveAsync(id);
            return NoContent();
        }

        [HttpPost("arrive")]
        public async Task<ActionResult<MonkeyDto>> AddMonkey([FromBody] CreateMonkeyDto dto)
        {
            var canArrive = await _monkeyRepository.CanMonkeyArriveAsync(DateTime.UtcNow);

            if (!canArrive)
            {
                return BadRequest("The maximum number of arrivals for today has been reached.");
            }

            var monkey = _mapper.Map<Monkey>(dto);
            monkey.ArrivalDate = DateTime.UtcNow;

            await _monkeyRepository.AddAsync(monkey);
            _reportService.InvalidateSpeciesCountCache();

            return Ok(_mapper.Map<MonkeyDto>(monkey));
        }

        [HttpDelete("{id}/leave")] //leaving monkey
        public async Task<IActionResult> RemoveMonkey(Guid id)
        {
            var monkey = await _monkeyRepository.GetByIdAsync(id);

            if (monkey == null)
            {
                return NotFound();
            }

            var canLeave = await _monkeyRepository.CanMonkeyLeaveAsync(DateTime.UtcNow, monkey.SpeciesId);

            if (!canLeave)
            {
                return BadRequest("You cannot remove the monkey due to departure restrictions.");
            }

            monkey.DepartureDate = DateTime.UtcNow;

            await _monkeyRepository.UpdateAsync(monkey);
            _reportService.InvalidateSpeciesCountCache();

            return NoContent();
        }
    }
}
