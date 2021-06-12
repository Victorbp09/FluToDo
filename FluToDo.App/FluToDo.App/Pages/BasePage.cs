
using FluToDo.App.ViewModels;
using Xamarin.Forms;

namespace FluToDo.App.Pages
{
    public class BasePage : ContentPage
    {
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var vm = this.BindingContext as BaseViewModel;
            if (vm != null)
            {
                vm.OnAppearing();
            }
        }
    }
}
