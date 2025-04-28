using MonkeyShelter.Console.Model;
using MonkeyShelter.Core.DTOs;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MonkeyShelter.Console
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var token = await LoginAndGetTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                System.Console.WriteLine("Autentification failed.");
                return;
            }

            System.Console.WriteLine("Ok login. Taken token:");
            System.Console.WriteLine(token.Substring(0, 50) + "...");

            await GetMonkeysAsync(token);
        }
 
        //Send login data and receive JWT token from the API
        public static async Task<string> LoginAndGetTokenAsync()
        {
            var client = new HttpClient();
            var loginUrl = "https://localhost:7284/api/auth/login";

            var loginRequest = new LoginRequest
            {
                Username = "manager1",
                Password = "Pass123!" 
            };

            var response = await client.PostAsJsonAsync(loginUrl, loginRequest);
            if (!response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("Login neuspešan: " + response.StatusCode);
                return null;
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return tokenResponse?.Token;
        }

        //Authorized call with token, in header Authorization for each request
        public static async Task GetMonkeysAsync(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:5001/api/monkey");
            if (!response.IsSuccessStatusCode)
            {
                System.Console.WriteLine("API call ERROR: " + response.StatusCode);
                return;
            }

            var monkeys = await response.Content.ReadFromJsonAsync<List<MonkeyDto>>();
            foreach (var monkey in monkeys)
            {
                System.Console.WriteLine($"🐒 {monkey.Name} - {monkey.Name}");
            }
        }
    }
}
