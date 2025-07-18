using System.Net.Http.Json;
using System.Text.Json;
using StudentTaskTechVegas.ApiServices.Interface;
using StudentTaskTechVegas.Models;

namespace StudentTaskTechVegas.ApiServices
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LoginResponse?> GetLoginDetailsAsync(LoginModel model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/LoginAuthentication", model);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<LoginResponse>(json);

            }

            return null;
        }
    }
}
