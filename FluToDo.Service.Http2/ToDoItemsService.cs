using FlueToDo.App.DTO;
using FluToDo.Service.Http.Interfaces;
using FluToDo.Service.Http.Models;
using FluToDo.Service.Http.Models.Mappers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
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
            var url = _baseUrl + "todo";
            var toDoItems = await GetAsync<IEnumerable<ToDoItemApiModel>>(url);
            return toDoItems.MapToToDoItems();
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var responseString = await _httpClient.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }
    }
}
