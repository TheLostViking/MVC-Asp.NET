using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using App.Interfaces;
using App.Models;
using Microsoft.Extensions.Configuration;

namespace App.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;

        public StudentService(IConfiguration config, HttpClient client)
        {
            _client = client;
            _baseUrl = config.GetSection("api:baseUrl").Value + "students";
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };
        }

        public async Task<bool> AddStudent(StudentModel model)
        {
            try
            {
                var url = _baseUrl;
                var data = JsonSerializer.Serialize(model);

                var response = await _client.PostAsync(url, new StringContent(data, Encoding.Default, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
              throw new Exception(ex.Message);
            }
        }

        public Task<bool> DeleteStudent(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> EditStudent(int id, StudentModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<StudentModel> GetStudentByEmailAsync(string email)
        {
            throw new System.NotImplementedException();
        }

        public async Task<StudentModel> GetStudentByIdAsync(int id)
        {
            var response = await _client.GetAsync($"{_baseUrl}/{id}");

            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<StudentModel>(data, _options);
                
                return result;                
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception("Hittade inte studenten!");
            }
        }

        public async Task<List<StudentModel>> GetStudentsAsync()
        {
            var response = await _client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<StudentModel>>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("That didnt quite work!");
            }
        }
    }
}