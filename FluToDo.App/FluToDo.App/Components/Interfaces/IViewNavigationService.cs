using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FluToDo.App.Components.Interfaces
{
    public interface IViewNavigationService
    {
        void Initialize(INavigation navigation, NavigationMapper navigationMapper);
        Task NavigateToAsync(object navigationSource, object parameter = null);
        Task GoBackAsync();
    }
}
