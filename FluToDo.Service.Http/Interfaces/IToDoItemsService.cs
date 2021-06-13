using FlueToDo.App.DTO;
using FlueToDo.App.DTO.ApiResponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluToDo.Service.Http.Interfaces
{
    public interface IToDoItemsService
    {
        Task<GetToDoItemsResponse> GetToDoItems();
        Task<BaseApiResponse> CreateToDoItem(ToDoItem item);
        Task<BaseApiResponse> DeleteToDoItem(string itemKey);
        Task<BaseApiResponse> UpdateToDoItem(ToDoItem item);
    }
}
