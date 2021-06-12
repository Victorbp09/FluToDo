using System.Windows.Input;
using Xamarin.Forms;
using FluToDo.App.Components.Navigation;
using FluToDo.Service.Http.Interfaces;
using FlueToDo.App.DTO;
using FluToDo.App.Components.Toast;

namespace FluToDo.App.ViewModels
{
    public class CreateToDoItemViewModel : BaseViewModel
    {
        private readonly IToDoItemsService _toDoItemsService;
        private readonly INavigator _navigator;

        public string NewToDoName { get; set; }

        public ICommand SaveNewToDoItemCommand { get; set; }

        public CreateToDoItemViewModel(INavigator navigator, IToast toast, IToDoItemsService toDoItemsService)
            : base(toast)
        {
            _navigator = navigator;
            _toDoItemsService = toDoItemsService;
            SaveNewToDoItemCommand = new Command(SaveNewToDoItem);
        }

        private async void SaveNewToDoItem()
        {
            if (IsValidName())
            {
                await _toDoItemsService.CreateToDoItem(new ToDoItem
                {
                    Name = NewToDoName
                });
                await _navigator.PopAsync();
            }
        }

        private bool IsValidName()
        {
            if (string.IsNullOrWhiteSpace(NewToDoName))
            {
                Toast("Name can't be empty");
                return false;
            }
            return true;
        }
    }
}
