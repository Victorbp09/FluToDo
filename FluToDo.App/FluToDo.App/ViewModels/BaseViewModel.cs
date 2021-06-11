using FluToDo.App.Components.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FluToDo.App.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
		private IToast _toast;
		public BaseViewModel(IToast toast)
        {
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
	}
}
