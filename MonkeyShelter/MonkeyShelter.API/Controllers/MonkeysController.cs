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
    /// <summary>
    /// Controller doesn't catch the exception given from service, it just forwards it to the custom exception middleware
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")] 
    public class MonkeysController : ControllerBase
    {
        private readonly IMonkeyService _monkeyService;

        public MonkeysController(IMonkeyService monkeyService)
        {
            _monkeyService = monkeyService;
        }

        /// <summary>
        /// Get all monkeys
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MonkeyDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MonkeyDto>>> GetAllMonkeys()
        {
            var monkeys = await _monkeyService.GetAllAsync();
            return Ok(monkeys);
        }

        /// <summary>
        /// Get monkey by Id or NotFound
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(MonkeyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MonkeyDto>> GetMonkeyAsync(Guid id)
        {
            var monkey = await _monkeyService.GetMonkeyAsync(id);

            return monkey == null ? NotFound() : Ok(monkey);
        }

        /// <summary>
        /// Update monkey by id, with new weight, then return 204-NoContent or forward exception to middleware
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newWeight"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}/weight")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateWeightAsync(Guid id, [FromBody] double newWeight)
        {
            await _monkeyService.UpdateWeightAsync(id, newWeight);
            return NoContent();
        }

        /// <summary>
        /// Delete monkey then return 204-NoContent/ forward exception to middleware
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
