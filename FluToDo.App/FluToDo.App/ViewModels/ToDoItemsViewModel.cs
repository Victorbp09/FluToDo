using FlueToDo.App.DTO;
using FluToDo.Service.Http.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;

namespace FluToDo.App.ViewModels
{
    public class ToDoItemsViewModel : BaseViewModel
    {
        private readonly IToDoItemsService _toDoItemsService;
        public ObservableCollection<ToDoItem> ToDoItems { get; set; }

        public ToDoItemsViewModel(IToDoItemsService toDoItemsService)
        {
            _toDoItemsService = toDoItemsService;
            ToDoItems = new ObservableCollection<ToDoItem>();
            LoadData();
        }

        private async void LoadData()
        {
            var toDoItems = await _toDoItemsService.GetToDoItems();
            ToDoItems = new ObservableCollection<ToDoItem>(toDoItems);
            OnPropertyChanged(nameof(ToDoItems));
        }
    }
}
