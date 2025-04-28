using MonkeyShelter.Core.DTOs;
using MonkeyShelter.Core.Interfaces;
using MonkeyShelter.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonkeyShelter.Core.DTOs.Reports;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace MonkeyShelter.Services.Reports
{
    public class ReportService: IReportService
    {
        private readonly MonkeyShelterDbContext _context;
        private readonly IDistributedCache _cache;

        private const string SpeciesCountCacheKey = "SpeciesCount";

        public ReportService(MonkeyShelterDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<IEnumerable<SpeciesCountDto>> GetMonkeyCountBySpeciesAsync()
        {
            var cachedData = await _cache.GetStringAsync(SpeciesCountCacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<SpeciesCountDto>>(cachedData);
            }

            var result = await _context.Monkeys
                .Include(m => m.Species)
                .GroupBy(m => m.Species!.Name)
                .Select(g => new SpeciesCountDto
                {
                    Species = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));  // We cache for an 1hour

            await _cache.SetStringAsync(SpeciesCountCacheKey, JsonConvert.SerializeObject(result), cacheEntryOptions);

            return result;
        }

        public void InvalidateSpeciesCountCache()
        {
            _cache.Remove(SpeciesCountCacheKey);
        }

        public async Task<IEnumerable<SpeciesCountDto>> GetArrivalsBySpeciesBetweenAsync(DateTime from, DateTime to)
        {
            return await _context.Monkeys
                .Include(m => m.Species)
                .Where(m => m.ArrivalDate >= from && m.ArrivalDate <= to)
                .GroupBy(m => m.Species!.Name)
                .Select(g => new SpeciesCountDto
                {
                    Species = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
        }

        //Information:
        //SetStringAsync =set data in cache
        //GetStringAsync-get data from cache
        //Remove-delete cache
    }
}
