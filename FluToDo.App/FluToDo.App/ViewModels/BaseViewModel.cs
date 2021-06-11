using FluToDo.App.Components.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using static FluToDo.App.Helpers.Enums;

namespace FluToDo.App.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
		private IToast _toast;
		private IViewNavigationService _navigationService;

		public BaseViewModel(IToast toast)
        {
			_navigationService = DependencyService.Get<IViewNavigationService>();
			_toast = toast;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChangedEventHandler propertyChanged = PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public virtual void OnAppearing()
		{

		}

		public void Toast(string message)
        {
			_toast.ShowMessage(message);
		}

		public async Task NavigateTo(NavigationPageSource page) 
		{
			await _navigationService.NavigateToAsync(NavigationPageSource.CreateToDoItemPage);
		}

		public async Task PopAsync()
		{
			await _navigationService.GoBackAsync();
		}
	}
}
