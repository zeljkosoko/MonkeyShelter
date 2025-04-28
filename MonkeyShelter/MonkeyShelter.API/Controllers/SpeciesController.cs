using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonkeyShelter.Core.DTOs;
using MonkeyShelter.Core.Entities;
using MonkeyShelter.Core.Interfaces;

namespace MonkeyShelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager")]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesRepository _repo;
        private readonly IMapper _mapper;

        public SpeciesController(
            ISpeciesRepository repo, 
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SpeciesDto>>> GetAll()
        {
            var species = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<SpeciesDto>>(species));
        }

        [HttpPost]
        public async Task<ActionResult<SpeciesDto>> Create(CreateSpeciesDto dto)
        {
            var species = _mapper.Map<Species>(dto);
            await _repo.AddAsync(species);

            return CreatedAtAction(nameof(GetAll), null, _mapper.Map<SpeciesDto>(species));
        }
    }
}
