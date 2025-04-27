using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MonkeyShelter.Core.DTOs.Reports;
using MonkeyShelter.Core.Interfaces;

namespace MonkeyShelter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("species-count")]
        public async Task<ActionResult<IEnumerable<SpeciesCountDto>>> GetSpeciesCount()
        {
            var result = await _reportService.GetMonkeyCountBySpeciesAsync();
            return Ok(result);
        }

        [HttpGet("arrivals")]
        public async Task<ActionResult<IEnumerable<SpeciesCountDto>>> GetArrivalsBySpeciesBetween(
            [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = await _reportService.GetArrivalsBySpeciesBetweenAsync(from, to);
            return Ok(result);
        }
    }
}
