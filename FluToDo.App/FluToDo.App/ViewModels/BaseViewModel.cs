using FluToDo.App.Components.Navigation;
using FluToDo.App.Components.Toast;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FluToDo.App.ViewModels
{
    public abstract class BaseViewModel : IViewModel
	{
		private IToast _toast;

        public string Title { get ; set ; }

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

		public void SetState<T>(Action<T> action) where T : class, IViewModel
		{
			action(this as T);
		}

		public void Toast(string message)
		{
			_toast.ShowMessage(message);
		}
    }
}
