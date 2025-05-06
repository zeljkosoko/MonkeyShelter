using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MonkeyShelter.Infrastructure.Repositories;
using MonkeyShelter.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using MonkeyShelter.Infrastructure;

namespace MonkeyShelter.Test
{
    public class MonkeyRepositoryTests
    {
        private readonly MonkeyShelterDbContext _context;
        private readonly MonkeyRepository _monkeyRepository;

        public MonkeyRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<MonkeyShelterDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) //unique db for each test
                .Options;

            _context = new MonkeyShelterDbContext(options);
            _monkeyRepository = new MonkeyRepository(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddMonkeyToDatabase()
        {
            // Arrange
            var species = new Species { Name = "Capuchin" };
            var shelter = new Shelter { Name = "Jungle Shelter" };

            //First add then save independed entities in db
            await _context.Species.AddAsync(species);
            await _context.Shelters.AddAsync(shelter);
            await _context.SaveChangesAsync();

            var monkey = new Monkey
            {
                Id = Guid.NewGuid(),
                Name = "Charlie",
                Weight = 10.2,
                ArrivalDate = DateTime.UtcNow,
                SpeciesId = species.Id,  
                ShelterId = shelter.Id  
            };

            // Act
            await _monkeyRepository.AddAsync(monkey);
            var result = await _monkeyRepository.GetByIdAsync(monkey.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Charlie", result.Name);
            Assert.Equal("Capuchin", result.Species.Name);
        }
}
}
