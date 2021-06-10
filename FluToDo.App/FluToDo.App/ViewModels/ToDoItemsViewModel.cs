using FlueToDo.App.DTO;
using FluToDo.App.Pages;
using FluToDo.Service.Http.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace FluToDo.App.ViewModels
{
    public class ToDoItemsViewModel : BaseViewModel
    {
        private readonly IToDoItemsService _toDoItemsService;
        public ObservableCollection<ToDoItem> ToDoItems { get; set; }

        public ICommand NavigateToNewToDoItemCommand { get; set; }

        public ToDoItemsViewModel(IToDoItemsService toDoItemsService)
        {
            _toDoItemsService = toDoItemsService;
            ToDoItems = new ObservableCollection<ToDoItem>();
            NavigateToNewToDoItemCommand = new Command(NavigateToNewToDoItem);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }

        private async void LoadData()
        {
            var toDoItems = await _toDoItemsService.GetToDoItems();
            ToDoItems = new ObservableCollection<ToDoItem>(toDoItems);
            OnPropertyChanged(nameof(ToDoItems));
        }

        private async void NavigateToNewToDoItem()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreateToDoItemPage());
        }
    }
}
