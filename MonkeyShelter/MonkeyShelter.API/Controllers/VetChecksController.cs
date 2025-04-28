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
    public class VetChecksController : ControllerBase
    {
        private readonly IVetCheckRepository _repo;
        private readonly IMapper _mapper;

        public VetChecksController(
            IVetCheckRepository repo, 
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VetCheckDto>>> GetAll()
        {
            var checks = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<VetCheckDto>>(checks));
        }

        [HttpPost]
        public async Task<ActionResult<VetCheckDto>> Add(CreateVetCheckDto dto)
        {
            var check = _mapper.Map<VetCheck>(dto);
            await _repo.AddAsync(check);

            return Ok(_mapper.Map<VetCheckDto>(check));
        }

        [HttpGet("missing")]
        public async Task<ActionResult<IEnumerable<MonkeyDto>>> GetMonkeysMissingChecks()
        {
            var monkeys = await _repo.GetMonkeysMissingCheckAsync();
            return Ok(_mapper.Map<IEnumerable<MonkeyDto>>(monkeys));
        }
    }
}
