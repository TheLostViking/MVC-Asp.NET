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
    public class CourseService : ICourseService
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _options;

        public CourseService(IConfiguration config, HttpClient client)
        {
            _client = client;
            _baseUrl = config.GetSection("api:baseUrl").Value + "courses"; 
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };
        }

        public async Task<List<CourseModel>> GetCoursesAsync()
        {            
            var response = await _client.GetAsync($"{_baseUrl}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<CourseModel>>(data, _options);
                
                return result;
            }
            else
            {
                throw new Exception("That didn't quite work!");
            }
        }

        public async Task<bool> AddCourse(CourseModel model)
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

        public async Task<bool> DeleteCourse(int courseNumber)
        {
            try
            {
                var course = await GetCourseByCourseNoAsync(courseNumber);
                var url = _baseUrl + $"/{course.CourseNumber}";
                var response = await _client.DeleteAsync(url);

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
        public async Task<bool> EditCourse(int id, CourseModel model)
        {
            try
            {
                var url = _baseUrl + $"/{id}";
                var data = JsonSerializer.Serialize(model);

                var response = await _client.PutAsync(url, new StringContent(data, Encoding.Default, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourseModel> GetCourseByCourseNoAsync(int courseNumber)
        {
            var response = await _client.GetAsync($"{_baseUrl}/find/{courseNumber}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CourseModel>(data, _options);
                return result;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception("Hittade inte kursen!");
            }
        }

        public async Task<CourseModel> GetCourseByIdAsync(int id)
        {
            var response = await _client.GetAsync($"{_baseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<CourseModel>(data, _options);

                return result;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else
            {
                throw new Exception("Hittade inte kursen");
            }
        }

        public async Task<List<CourseModel>> GetActiveCoursesAsync()
        {
           var response = await _client.GetAsync($"{_baseUrl}/status/active");
           if(response.IsSuccessStatusCode)
           {
               var data = await response.Content.ReadAsStringAsync();
               var result = JsonSerializer.Deserialize<List<CourseModel>>(data, _options);

               return result;
           }
           else
           {
               throw new Exception("Something went wrong!");
           }
        }

        public async Task<List<LevelModel>> GetLevelsAsync()
        {
            var response = await _client.GetAsync($"{_baseUrl}/levels");
            if(response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<List<LevelModel>>(data, _options);
                return result;
            }
            else
            {
                throw new Exception("Hittade inga levels");
            }            

        }

        public async Task<bool> SetCourseAsInactiveAsync(int id)
        {
            try
            {
                var url = _baseUrl +$"/{id}";
                var data = JsonSerializer.Serialize(id);

                var response = await _client.PatchAsync(url, new StringContent(data, Encoding.Default, "application/json"));
                if(response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = response.Content.ReadAsStringAsync().Result;
                    throw new Exception(error);
                }
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);
            }
        }
    }
}