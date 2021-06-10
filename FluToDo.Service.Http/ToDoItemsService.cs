using FlueToDo.App.DTO;
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

        public async Task<List<ToDoItem>> GetToDoItems()
        {
            var url = "todo";
            var toDoItems = await GetAsync<IEnumerable<ToDoItemApiModel>>(url);
            return toDoItems.MapToToDoItems();
        }

        public async Task CreateToDoItem(ToDoItem item)
        {
            var url = "todo";
            var toDoItemApiModel = item.MapToToDoItemApiModel();
            var body = JsonConvert.SerializeObject(toDoItemApiModel);
            await PostAsync(url, body);
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var responseString = await _httpClient.GetStringAsync(_baseUrl + url);
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }

        private async Task PostAsync(string url, string body)
        {
            StringContent postContent = new StringContent(body, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(String.Format("{0}{1}", _baseUrl, url), postContent);
        }
    }
}
