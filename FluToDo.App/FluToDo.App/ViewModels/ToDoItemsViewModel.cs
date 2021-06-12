using FlueToDo.App.DTO;
using FluToDo.App.Components.Navigation;
using FluToDo.App.Components.Toast;
using FluToDo.Service.Http.Interfaces;
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
        public bool EmptyToDoItemsList { get { return !IsLoading && ( ToDoItems == null || !ToDoItems.Any()) ;  } }

        public ICommand NavigateToNewToDoItemCommand { get; set; }
        public ICommand DeleteToDoItemCommand { get; set; }

        public bool IsLoading { get; set; }

        public ToDoItemsViewModel(IToast toast, INavigator navigator, IToDoItemsService toDoItemsService)
            : base(toast)
        {
            _navigator = navigator;
            _toDoItemsService = toDoItemsService;
            ToDoItems = new ObservableCollection<ToDoItem>();
            NavigateToNewToDoItemCommand = new Command(NavigateToNewToDoItem);
            DeleteToDoItemCommand = new Command(async (x) => await DeleteToDoItem(x));
        }

        public override async void OnAppearing()
        {
            UpdateLoadingComponents(isLoading: true);
            base.OnAppearing();
            await LoadData();
            UpdateLoadingComponents(isLoading: false);
        }

        private async Task LoadData()
        {
            var toDoItems = await _toDoItemsService.GetToDoItems();
            ToDoItems = new ObservableCollection<ToDoItem>(toDoItems);
            OnPropertyChanged(nameof(ToDoItems));
        }

        private async void NavigateToNewToDoItem()
        {
            await _navigator.PushAsync<CreateToDoItemViewModel>();
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

        private void UpdateLoadingComponents(bool isLoading)
        {
            IsLoading = isLoading;
            OnPropertyChanged(nameof(IsLoading));
            OnPropertyChanged(nameof(EmptyToDoItemsList));
        }
    }
}
