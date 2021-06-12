using FluToDo.App.Ioc;

namespace FluToDo.App
{
    public partial class App 
    {
        public App()
        {
            InitializeComponent();
            var bootstrapper = new Bootstrapper(this);
            bootstrapper.Run();
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
