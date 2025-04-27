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
    public class MonkeyControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public MonkeyControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Уклони постојећи DbContext
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<MonkeyShelterDbContext>));
                    if (descriptor != null)
                        services.Remove(descriptor);

                    // Креирај in-memory базу
                    services.AddDbContext<MonkeyShelterDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("IntegrationTestDb");
                    });

                    // Напуни базу обавезним подацима
                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<MonkeyShelterDbContext>();

                    db.Database.EnsureCreated();

                    var species = new Species { Id = 1, Name = "Capuchin" };
                    var shelter = new Shelter { Id = 1, Name = "Rainforest Shelter"};

                    db.Species.Add(species);
                    db.Shelters.Add(shelter);
                    db.SaveChanges();
                });
            }).CreateClient();
        }

        [Fact]
        public async Task AddMonkey_ReturnsCreatedStatus_AndCorrectData()
        {
            // Arrange
            var monkeyDto = new MonkeyDto
            {
                Name = "Lola",
                Weight = 11.5,
                ArrivalDate = DateTime.UtcNow
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/monkeys", monkeyDto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var createdMonkey = await response.Content.ReadFromJsonAsync<MonkeyDto>();
            Assert.NotNull(createdMonkey);
            Assert.Equal("Lola", createdMonkey?.Name);
            Assert.Equal(11.5, createdMonkey?.Weight);
        }
    }
}
