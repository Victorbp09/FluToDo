﻿using FlueToDo.App.DTO;
using System.Collections.Generic;

namespace FluToDo.Service.Http.Models.Mappers
{
    public static class ToDoItemMapper
    {
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

        public static ToDoItem MapToToDoItem(this ToDoItemApiModel toDoItem)
        {
            return new ToDoItem
            {
                Name = toDoItem.Name,
                IsComplete = toDoItem.IsComplete
            };
        }
    }
}
