using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonkeyShelter.Core.DTOs;
using MonkeyShelter.Core.Entities;
using MonkeyShelter.Core.Interfaces;

namespace MonkeyShelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonkeysController : ControllerBase
    {
        private readonly IMonkeyRepository _monkeyRepository;
        private readonly IMapper _mapper;

        public MonkeysController(IMonkeyRepository monkeyRepository, IMapper mapper)
        {
            _monkeyRepository = monkeyRepository;
            _mapper = mapper;
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
    }
}
