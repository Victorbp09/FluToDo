using FlueToDo.App.DTO;
using FluToDo.App.Components.Interfaces;
using FluToDo.App.Pages;
using FluToDo.Service.Http.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FluToDo.App.ViewModels
{
    public class ToDoItemsViewModel : BaseViewModel
    {
        private readonly IToDoItemsService _toDoItemsService;
        public ObservableCollection<ToDoItem> ToDoItems { get; set; }
        public bool EmptyToDoItemsList { get { return ToDoItems == null || !ToDoItems.Any(); } }

        public ICommand NavigateToNewToDoItemCommand { get; set; }
        public ICommand DeleteToDoItemCommand { get; set; }

        public ToDoItemsViewModel(IToDoItemsService toDoItemsService, IToast toast)
            : base(toast)
        {
            _toDoItemsService = toDoItemsService;
            ToDoItems = new ObservableCollection<ToDoItem>();
            NavigateToNewToDoItemCommand = new Command(NavigateToNewToDoItem);
            DeleteToDoItemCommand = new Command(async (x) => await DeleteToDoItem(x));
        }

        public async override void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        private async Task LoadData()
        {
            var toDoItems = await _toDoItemsService.GetToDoItems();
            ToDoItems = new ObservableCollection<ToDoItem>(toDoItems);
            OnPropertyChanged(nameof(ToDoItems));
            OnPropertyChanged(nameof(EmptyToDoItemsList));
        }

        private async void NavigateToNewToDoItem()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreateToDoItemPage());
        }

        private async Task DeleteToDoItem(object toDoItem)
        {
            if (IsValidElementToRemove(toDoItem))
            {
                var item = toDoItem as ToDoItem;
                await _toDoItemsService.DeleteToDoItem(item.Key);
                await LoadData();
            }
        }

        private bool IsValidElementToRemove(object toDoItem)
        {
            if (toDoItem is ToDoItem)
            {
                var item = toDoItem as ToDoItem;
                if (!string.IsNullOrWhiteSpace(item.Key)) 
                {
                    return true;
                }
            }
            Toast("ToDo item couldn't be deleted");
            return false;
        }
    }
}
