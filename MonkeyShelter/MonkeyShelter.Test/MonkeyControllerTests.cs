using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using MonkeyShelter.API;
using MonkeyShelter.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonkeyShelter.Infrastructure;
using Xunit;
using MonkeyShelter.Core.DTOs;

namespace MonkeyShelter.Test
{
    public class MonkeyControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MonkeyControllerTests(WebApplicationFactory<Program> factory)
        {
            //We are customizing the factory to use an In-Memory database.
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove existing DbContext
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<MonkeyShelterDbContext>));

                    if (descriptor != null)
                        services.Remove(descriptor);

                    // Add a new DbContext with an In-Memory database
                    services.AddDbContext<MonkeyShelterDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                    });

                    // Populate the test with data
                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<MonkeyShelterDbContext>();

                    var species = new Species { Name = "Capuchin" };
                    var shelter = new Shelter { Name = "Main Shelter" };
                    context.Species.Add(species);
                    context.Shelters.Add(shelter);
                    context.SaveChanges();

                    context.Monkeys.Add(new Monkey
                    {
                        Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                        Name = "Charlie",
                        Weight = 12.3,
                        ArrivalDate = DateTime.UtcNow,
                        SpeciesId = species.Id,
                        ShelterId = shelter.Id
                    });
                    context.SaveChanges();
                });
            }).CreateClient();
        }

        [Fact]
        public async Task GetById_ReturnsMonkeyDto_WhenMonkeyExists()
        {
            // Act
            var response = await _client.GetAsync("/api/monkeys/11111111-1111-1111-1111-111111111111");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var monkey = await response.Content.ReadFromJsonAsync<MonkeyDto>();
            Assert.NotNull(monkey);
            Assert.Equal("Charlie", monkey?.Name);
        }
    }
}
