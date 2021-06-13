using FlueToDo.App.DTO;
using FluToDo.App.Components.Navigation;
using FluToDo.App.Components.Toast;
using FluToDo.Service.Http.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FluToDo.App.ViewModels
{
    public class ToDoItemsViewModel : BaseViewModel
    {
        private readonly INavigator _navigator;
        private readonly IToDoItemsService _toDoItemsService;

        public ObservableCollection<ToDoItem> ToDoItems { get; set; }
        public bool EmptyToDoItemsList { get { return !IsLoading && (ToDoItems == null || !ToDoItems.Any()); } }

        public ICommand NavigateToNewToDoItemCommand { get; set; }
        public ICommand DeleteToDoItemCommand { get; set; }
        public ICommand RefreshToDoItemsCommand { get; set; }

        private ToDoItem _selectedItem;
        public ToDoItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                if (_selectedItem != null)
                {
                    var tst = _selectedItem;
                    EditItemCompleted(_selectedItem);
                    _selectedItem = null;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }
        public bool IsLoading { get; set; }
        public bool ItemsRefreshing { get; set; }

        public ToDoItemsViewModel(IToast toast, INavigator navigator, IToDoItemsService toDoItemsService)
            : base(toast)
        {
            _navigator = navigator;
            _toDoItemsService = toDoItemsService;
            ToDoItems = new ObservableCollection<ToDoItem>();
            NavigateToNewToDoItemCommand = new Command(NavigateToNewToDoItem);
            DeleteToDoItemCommand = new Command(async (x) => await DeleteToDoItem(x));
            RefreshToDoItemsCommand = new Command(RefreshToDoItems);
        }

        public override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadData();
        }

        // Load ToDo items list data
        private async Task LoadData()
        {
            UpdateLoadingComponents(isLoading: true);
            var getToDoItemsResponse = await _toDoItemsService.GetToDoItems();

            if (!getToDoItemsResponse.IsSuccess)
            {
                Toast("Connection error");
                return;
            }

            UpdateToDoItemsList(getToDoItemsResponse.Items);
            UpdateLoadingComponents(isLoading: false);
        }

        // Navigate to create ToDo item page
        private async void NavigateToNewToDoItem()
        {
            await _navigator.PushAsync<CreateToDoItemViewModel>();
        }

        // Delete To Do item
        private async Task DeleteToDoItem(object toDoItem)
        {
            if (IsValidElementToRemove(toDoItem))
            {
                var item = toDoItem as ToDoItem;
                var deleteItemResponse = await _toDoItemsService.DeleteToDoItem(item.Key);
                
                if (!deleteItemResponse.IsSuccess)
                {
                    Toast("Connection error");
                    return;
                }

                Toast($"ToDo item {item.Name} has been deleted correctly");
                await LoadData();
                UpdateLoadingComponents(isLoading: false);
            }
        }

        // Check To Do items has a valid key
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

        // Update loading components visibility (spinner and empty list message)
        private void UpdateLoadingComponents(bool isLoading)
        {
            IsLoading = isLoading;
            OnPropertyChanged(nameof(IsLoading));
            OnPropertyChanged(nameof(EmptyToDoItemsList));
        }

        // Invert value of IsComplete property of ToDo item. 
        private async void EditItemCompleted(ToDoItem item)
        {
            item.IsComplete = !item.IsComplete;
            var editToDoItemResponse = await _toDoItemsService.UpdateToDoItem(item);

            if (!editToDoItemResponse.IsSuccess)
            {
                Toast("Connection error");
                return;
            }

            ToDoItems.FirstOrDefault(x => x.Key == item.Key).IsComplete = item.IsComplete;
            UpdateToDoItemsList(ToDoItems.ToList());
        }

        // Update the items of the ObservableCollection ToDoItems
        private void UpdateToDoItemsList(List<ToDoItem> items)
        {
            ToDoItems = new ObservableCollection<ToDoItem>(items);
            OnPropertyChanged(nameof(ToDoItems));
        }

        // Update list of items after performing a pull-to-refresh
        private async void RefreshToDoItems()
        {
            await LoadData();
            ItemsRefreshing = false;
            OnPropertyChanged(nameof(ItemsRefreshing));
        }
    }
}
