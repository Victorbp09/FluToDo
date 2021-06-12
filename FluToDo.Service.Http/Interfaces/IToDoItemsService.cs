﻿using FlueToDo.App.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FluToDo.Service.Http.Interfaces
{
    public interface IToDoItemsService
    {
        Task<List<ToDoItem>> GetToDoItems();
        Task CreateToDoItem(ToDoItem item);
        Task DeleteToDoItem(string itemKey);
        Task UpdateToDoItem(ToDoItem item);
    }
}
