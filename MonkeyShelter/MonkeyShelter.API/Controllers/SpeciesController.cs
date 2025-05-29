using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonkeyShelter.Core.DTOs;
using MonkeyShelter.Core.Entities;
using MonkeyShelter.Core.Interfaces;
using MonkeyShelter.Services.BusinessLogic.Abstractions;

namespace MonkeyShelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesService _speciesService;
        private readonly IMapper _mapper;

        public SpeciesController( ISpeciesService speciesService, IMapper mapper)
        {
            _speciesService = speciesService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SpeciesDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SpeciesDto>>> GetAll()
        {
            var species = await _speciesService.GetAllSpeciesAsync();

            return Ok(_mapper.Map<IEnumerable<SpeciesDto>>(species));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SpeciesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpeciesDto>> GetSpecies(int id)
        {
            var species = await _speciesService.GetSpeciesByAsync(id);

            return species != null ? Ok(species) : NotFound();
        }

        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        //Before contructor execution, FluentValidator automatically validate model CreateSpecies
        //If validation failed, ASP.NET CORE returns 400-BadRequest with validator message
        public async Task<ActionResult> CreateSpecies(CreateSpeciesDto speciesDto)
        {
            await _speciesService.CreateAsync(speciesDto);

            return Ok();
        }
    }
}
