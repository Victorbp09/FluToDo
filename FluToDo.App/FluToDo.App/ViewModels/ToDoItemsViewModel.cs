using FluToDo.Service.Http.Interfaces;

namespace FluToDo.App.ViewModels
{
    public class ToDoItemsViewModel
    {
        private readonly IToDoItemsService _toDoItemsService;

        public ToDoItemsViewModel(IToDoItemsService toDoItemsService)
        {
            _toDoItemsService = toDoItemsService;
            _toDoItemsService.GetToDoItemsMocked();
        }
    }
}
