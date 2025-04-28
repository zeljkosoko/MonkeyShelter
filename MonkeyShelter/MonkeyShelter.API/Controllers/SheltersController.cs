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
    public class SheltersController : ControllerBase
    {
        private readonly IShelterRepository _repo;
        private readonly IMapper _mapper;

        public SheltersController(
            IShelterRepository repo, 
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShelterDto>>> GetAll()
        {
            var shelters = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ShelterDto>>(shelters));
        }

        [HttpPost]
        public async Task<ActionResult<ShelterDto>> Create(CreateShelterDto dto)
        {
            var shelter = _mapper.Map<Shelter>(dto);
            await _repo.AddAsync(shelter);

            return CreatedAtAction(nameof(GetAll), null, _mapper.Map<ShelterDto>(shelter));
        }
    }
}
