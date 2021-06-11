using FluToDo.App.Components;
using FluToDo.App.Components.Interfaces;
using FluToDo.App.Ioc;
using FluToDo.App.Pages;
using Xamarin.Forms;
using static FluToDo.App.Helpers.Enums;

namespace FluToDo.App
{
    public partial class App 
    {
        public App()
        {
            InitializeComponent();
            var startPage = new ToDoItemsPage();
            InitializeNavigation(startPage);
            MainPage = new NavigationPage(startPage);
        }

        private void InitializeNavigation(Page startPage)
        {
            var mapper = new NavigationMapper();
            mapper.AddMapping(typeof(ToDoItemsPage), NavigationPageSource.ToDoItemsPage);
            mapper.AddMapping(typeof(CreateToDoItemPage), NavigationPageSource.CreateToDoItemPage);

            var navigationService = DependencyService.Get<IViewNavigationService>();
            navigationService.Initialize(startPage.Navigation, mapper);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
