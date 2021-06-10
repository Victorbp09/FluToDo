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
        private readonly string _baseUrl;

        public ToDoItemsService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<IEnumerable<ToDoApiModel>> GetToDoItems()
        {
            var url = _baseUrl + "";
            var toDoItems = await GetAsync<IEnumerable<ToDoApiModel>>(url);
            return toDoItems;
        }

        public async Task<IEnumerable<ToDoApiModel>> GetToDoItemsMocked()
        {
            return new List<ToDoApiModel>
            {
                new ToDoApiModel
                {
                    Name = "hola"
                }
            };
        }

        private async Task<T> GetAsync<T>(string url)
        {
            var responseString = await _httpClient.GetStringAsync(url);
            var result = JsonConvert.DeserializeObject<T>(responseString);
            return result;
        }
    }
}
