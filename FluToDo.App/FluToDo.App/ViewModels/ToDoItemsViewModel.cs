using FlueToDo.App.DTO;
using FluToDo.App.Components.Interfaces;
using FluToDo.App.Pages;
using FluToDo.Service.Http.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
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

        public ToDoItemsViewModel(IToDoItemsService toDoItemsService)
        {
            _toDoItemsService = toDoItemsService;
            ToDoItems = new ObservableCollection<ToDoItem>();
            NavigateToNewToDoItemCommand = new Command(NavigateToNewToDoItem);
            DeleteToDoItemCommand = new Command(DeleteToDoItem);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var toDoItems = await _toDoItemsService.GetToDoItems();
                ToDoItems = new ObservableCollection<ToDoItem>(toDoItems);
                OnPropertyChanged(nameof(ToDoItems));
                OnPropertyChanged(nameof(EmptyToDoItemsList));
            }
            catch (HttpRequestException ex)
            {
                DependencyService.Get<IToast>().ShowMessage("Error obtaining ToDo items.");
            }
        }

        private async void NavigateToNewToDoItem()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreateToDoItemPage());
        }

        private async void DeleteToDoItem(object toDoItem)
        {
            if (IsValidElementToRemove(toDoItem))
            {
                var item = toDoItem as ToDoItem;
                try
                {
                    await _toDoItemsService.DeleteToDoItem(item.Key);
                }
                catch (HttpRequestException ex)
                {
                    DependencyService.Get<IToast>().ShowMessage("Error deleting ToDo item.");
                    return;
                }
                LoadData();
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
            DependencyService.Get<IToast>().ShowMessage("ToDo item couldn't be deleted");
            return false;
        }
    }
}
