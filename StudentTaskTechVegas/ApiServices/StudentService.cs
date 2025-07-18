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
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;

        public StudentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<StudentModel>?> GetStudentsAsync(string token, int parentId)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                $"api/Students?ParentId={parentId}");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<StudentModel>>(json);
            }


            return null;
        }

    }
}
