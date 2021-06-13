using FlueToDo.App.DTO;
using System.Collections.Generic;

namespace FluToDo.Service.Http.Models.Mappers
{
    public static class ToDoItemMapper
    {
        // Convert a list of ToDoItemApiModel in a list of ToDoItem
        public static List<ToDoItem> MapToToDoItems(this IEnumerable<ToDoItemApiModel> toDoItems)
        {
            var mappedItes = new List<ToDoItem>();
            
            if (toDoItems != null)
            {
                foreach (var toDoItem in toDoItems)
                {
                    mappedItes.Add(toDoItem.MapToToDoItem());
                }
            }
            return mappedItes;
        }

        // Convert a ToDoItemApiModel in a ToDoItem
        public static ToDoItem MapToToDoItem(this ToDoItemApiModel toDoItem)
        {
            return new ToDoItem
            {
                Key = toDoItem.Key,
                Name = toDoItem.Name,
                IsComplete = toDoItem.IsComplete
            };
        }

        // Convert a ToDoItem in a ToDoItemApiModel
        public static ToDoItemApiModel MapToToDoItemApiModel(this ToDoItem toDoItem)
        {
            return new ToDoItemApiModel
            {
                Key = toDoItem.Key,
                Name = toDoItem.Name,
                IsComplete = toDoItem.IsComplete
            };
        }
    }
}
