using FluToDo.Service.Http.Interfaces;
using FluToDo.Service.Http.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluToDo.Service.Http
{
    public class ToDoItemsService : IToDoItemsService
    {
        private readonly HttpClient _httpClient;

        public ToDoItemsService(HttpClient httpClient, string baseUrl)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ToDoApiModel>> GetToDoItems()
        {
            var url = "";
            var toDoItems = await GetAsync<IEnumerable<ToDoApiModel>>(url);
            return toDoItems;
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var responseString = await _httpClient.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }
    }
}
