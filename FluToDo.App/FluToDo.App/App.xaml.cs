using FluToDo.App.Ioc;
using FluToDo.App.Pages;
using Xamarin.Forms;

namespace FluToDo.App
{
    public partial class App 
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ToDoItemsPage();
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
