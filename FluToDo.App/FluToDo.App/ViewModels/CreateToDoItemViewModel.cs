using FluToDo.Service.Http.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;
using FlueToDo.App.DTO;
using System;
using FluToDo.App.Components.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace FluToDo.App.ViewModels
{
    public class CreateToDoItemViewModel : BaseViewModel
    {
        private readonly IToDoItemsService _toDoItemsService;

        public string NewToDoName { get; set; }

        public ICommand SaveNewToDoItemCommand { get; set; }

        public CreateToDoItemViewModel(IToDoItemsService toDoItemsService, IToast toast)
            : base(toast)
        {
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
                await NavigateToMainPage();
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

        private async Task NavigateToMainPage()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
