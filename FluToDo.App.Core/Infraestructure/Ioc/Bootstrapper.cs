using Autofac;

namespace FluToDo.App.Core.Infraestructure.Ioc
{
    public abstract class Bootstrapper
    {
        private static FluToDoApplication _application;
        private static IContainer _container;

        public void Run(FluToDoApplication application)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                _application = application;

                var builder = new ContainerBuilder();
                _container = builder.Build();

                _application.Container = _container;
                application.OnAppStartup(_container);
            });
        }
    }
}
