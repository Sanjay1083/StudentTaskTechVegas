using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using StudentTaskTechVegas.ApiServices.Interface;
using StudentTaskTechVegas.Models;

namespace StudentTaskTechVegas.ApiServices
{
    public class ProfileService : IProfileService
    {
        private readonly HttpClient _httpClient;

        public ProfileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProfileResponse?> GetProfileAsync(string token, string parentId)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/ProfileInformation?parentId={parentId}"
            );

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"API Error: {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json))
            {
                Console.WriteLine("API returned empty response.");
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<ProfileResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization failed: {ex.Message}");
                Console.WriteLine("Raw JSON: " + json);
                return null;
            }
        }

    }
}
