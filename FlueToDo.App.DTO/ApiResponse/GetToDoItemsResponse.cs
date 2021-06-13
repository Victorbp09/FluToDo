using System.Collections.Generic;

namespace FlueToDo.App.DTO.ApiResponse
{
    public class GetToDoItemsResponse : BaseApiResponse
    {
        public List<ToDoItem> Items { get; set; }
    }
}
