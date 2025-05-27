using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonkeyShelter.Core.DTOs;
using MonkeyShelter.Core.Entities;
using MonkeyShelter.Core.Interfaces;
using MonkeyShelter.Services.BusinessLogic.Abstractions;
using MonkeyShelter.Services.Reports;

namespace MonkeyShelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")] 
    public class MonkeysController : ControllerBase
    {
        private readonly IMonkeyService _monkeyService;
        private readonly IReportService _reportService;

        public MonkeysController(IMonkeyService monkeyService, IReportService reportService)
        {
            _monkeyService = monkeyService;
            _reportService = reportService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<MonkeyDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IList<MonkeyDto>>> GetAllMonkeys()
        {
            var monkeys = await _monkeyService.GetAllAsync();
            return Ok(monkeys);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(MonkeyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MonkeyDto>> GetMonkeyAsync(Guid id)
        {
            var monkey = await _monkeyService.GetMonkeyAsync(id);

            return monkey == null ? NotFound() : Ok(monkey);
        }

        [HttpPut("{id:guid}/weight")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateWeightAsync(Guid id, [FromBody] double newWeight)
        {
            await _monkeyService.UpdateWeightAsync(id, newWeight);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMonkeyAsync(Guid id)
        {
            await _monkeyService.DeleteMonkeyAsync(id);
            return NoContent();
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(MonkeyDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<MonkeyDto>> RegisterArrival([FromBody] CreateMonkeyDto createMonkeyDto)
        {
            var createdMonkey = await _monkeyService.RegisterMonkeyArrivalAsync(createMonkeyDto);

            return CreatedAtAction(nameof(GetMonkeyAsync), new { id = createdMonkey.Id }, createdMonkey);
        }

        [HttpPut("{id:guid}/departure")]
        [ProducesResponseType(typeof(MonkeyDto[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<MonkeyDto>> RegisterDeparture(Guid id)
        {
            var departedMonkey = await _monkeyService.RegisterMonkeyDepartureAsync(id);

            return Ok(departedMonkey);
        }
    }
}
