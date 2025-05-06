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
using Microsoft.AspNetCore.Authorization;

namespace MonkeyShelter.Test
{
    public class MonkeyControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public MonkeyControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RemoveMonkey_ShouldReturnNoContent_WhenMonkeyExists()
        {
            var monkeyId = "4347caa1-302a-4d91-b1f6-29a82ba3e956";

            var response = await _client.DeleteAsync($"/api/monkeys/{monkeyId}");

            response.EnsureSuccessStatusCode();  // Checks the status= 204 (NoContent)
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
