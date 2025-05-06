using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MonkeyShelter.Core.Entities;
using MonkeyShelter.Core.Interfaces;
using MonkeyShelter.Infrastructure;
using MonkeyShelter.Services.Reports;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyShelter.Test
{
    public class ReportServiceTests
    {
        private readonly IReportService _reportService;
        private readonly Mock<IDistributedCache> _cacheMock;
        private readonly MonkeyShelterDbContext _context;

        public ReportServiceTests()
        {
            var options = new DbContextOptionsBuilder<MonkeyShelterDbContext>()
            .UseInMemoryDatabase(databaseName: "MonkeyShelterTest")
                .Options;

            _context = new MonkeyShelterDbContext(options);
            _cacheMock = new Mock<IDistributedCache>();
            _reportService = new ReportService(_context, _cacheMock.Object);
        }

        [Fact]
        public async Task GetMonkeyCountBySpeciesAsync_ShouldReturnSpeciesCount()
        {
            var species = new Species { Name = "Chimpanzee" };
            var shelter = new Shelter { Name = "Jungle Shelter" };

            _context.Species.Add(species);

            var monkey1 = new Monkey
            {
                Id = Guid.NewGuid(),
                Name = "George",
                Weight = 15.5,
                ArrivalDate = DateTime.UtcNow,
                SpeciesId = species.Id,
                ShelterId = shelter.Id
            };
            var shelter2 = new Shelter { Name = "Town Shelter" };

            var monkey2 = new Monkey
            {
                Id = Guid.NewGuid(),
                Name = "Mickey",
                Weight = 18.2,
                ArrivalDate = DateTime.UtcNow,
                SpeciesId = species.Id,
                ShelterId = shelter2.Id
            };

            await _context.Monkeys.AddRangeAsync(monkey1, monkey2);
            await _context.SaveChangesAsync();

            var result = await _reportService.GetMonkeyCountBySpeciesAsync();

            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Chimpanzee", result.First().Species);
            Assert.Equal(2, result.First().Count);
        }
    }
}
