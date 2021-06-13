using FlueToDo.App.DTO;
using FlueToDo.App.DTO.ApiResponse;
using FluToDo.Service.Http.Interfaces;
using FluToDo.Service.Http.Models;
using FluToDo.Service.Http.Models.Mappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FluToDo.Service.Http
{
    public class ToDoItemsService : IToDoItemsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ToDoItemsService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        // Get the list of ToDo items
        public async Task<GetToDoItemsResponse> GetToDoItems()
        {
            try
            {
                var url = "todo";
                var toDoItems = await GetAsync<IEnumerable<ToDoItemApiModel>>(url);
                return new GetToDoItemsResponse
                {
                    Items = toDoItems.MapToToDoItems()
                };
            }
            catch (HttpRequestException ex)
            {
                return new GetToDoItemsResponse { IsSuccess = false };
            }
        }

        // Create a ToDo item
        public async Task<BaseApiResponse> CreateToDoItem(ToDoItem item)
        {
            var url = "todo";
            var toDoItemApiModel = item.MapToToDoItemApiModel();
            var body = JsonConvert.SerializeObject(toDoItemApiModel);
            bool success = await PostAsync(url, body);
            return new BaseApiResponse
            {
                IsSuccess = success
            };
        }

        // Delete a ToDo item
        public async Task<BaseApiResponse> DeleteToDoItem(string itemKey)
        {
            var url = $"todo?id={itemKey}";
            bool success = await DeleteAsync(url);
            return new BaseApiResponse
            {
                IsSuccess = success
            };
        }
        
        // Update a ToDo item
        public async Task<BaseApiResponse> UpdateToDoItem(ToDoItem item)
        {
            var url = $"todo/{item.Key}";
            var toDoItemApiModel = item.MapToToDoItemApiModel();
            var body = JsonConvert.SerializeObject(toDoItemApiModel);
            bool success = await PutAsync(url, body);
            return new BaseApiResponse
            {
                IsSuccess = success
            };
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var responseString = await _httpClient.GetStringAsync(_baseUrl + url);
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        // Bad implementation due error in the ToDo API (always returns 500).
        // Should return response.IsSuccessStatusCode or false in the catch block
        private async Task<bool> PostAsync(string url, string body)
        {
            try
            {
                StringContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(String.Format("{0}{1}", _baseUrl, url), postContent);
                return true;
            }
            catch (HttpRequestException ex)
            {
                return true;
            }
        }

        private async Task<bool> DeleteAsync(string url)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_baseUrl + url);
                return response.IsSuccessStatusCode;
            }
            catch(HttpRequestException ex)
            {
                return false;
            }
        }

        private async Task<bool> PutAsync(string url, string body)
        {
            try
            {
                StringContent putContent = new StringContent(body, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(String.Format("{0}{1}", _baseUrl, url), putContent);
                return response.IsSuccessStatusCode;
            }
            catch(HttpRequestException ex)
            {
                return false;
            }
}
    }
}
