using FluToDo.App.Components;
using FluToDo.App.Components.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(NavigationService))]
namespace FluToDo.App.Components
{
    public class NavigationService : IViewNavigationService
    {
        private INavigation _navigation;
        private NavigationMapper _navigationMapper;

        public void Initialize(INavigation navigation, NavigationMapper navigationMapper)
        {
            _navigation = navigation;
            _navigationMapper = navigationMapper;
        }

        public async Task NavigateToAsync(object navigationSource, object parameter = null)
        {
            CheckIsInitialized();

            var type = _navigationMapper.GetTypeSource(navigationSource);

            if (type == null)
            {
                throw new InvalidOperationException(
                    "Can't find associated type for " + navigationSource.ToString());
            }

            ConstructorInfo constructor;
            object[] parameters;

            if (parameter == null)
            {
                constructor = type.GetTypeInfo()
                                  .DeclaredConstructors
                                  .FirstOrDefault(c => !c.GetParameters().Any());

                parameters = new object[] { };
            }
            else
            {
                constructor = type.GetTypeInfo()
                                  .DeclaredConstructors
                                  .FirstOrDefault(c =>
                                  {
                                      var p = c.GetParameters();
                                      return p.Count() == 1 &&
                                          p[0].ParameterType == parameter.GetType();
                                  });

                parameters = new[] { parameter };
            }

            if (constructor == null)
            {
                throw new InvalidOperationException(
                    "No suitable constructor found for page " + navigationSource.ToString());
            }

            var page = constructor.Invoke(parameters) as Page;

            await _navigation.PushAsync(page);
        }

        public async Task GoBackAsync()
        {
            CheckIsInitialized();

            await _navigation.PopAsync();
        }

        private void CheckIsInitialized()
        {
            if (_navigation == null || _navigationMapper == null)
                throw new NullReferenceException("Call Initialize method first.");
        }
    }
}
