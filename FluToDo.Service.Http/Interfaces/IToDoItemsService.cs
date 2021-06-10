using FluToDo.Service.Http.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluToDo.Service.Http.Interfaces
{
    public interface IToDoItemsService
    {
        Task<IEnumerable<ToDoApiModel>> GetToDoItems();
    }
}
