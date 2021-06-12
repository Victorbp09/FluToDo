using Autofac;
using FluToDo.App.Components.Navigation;

namespace FluToDo.App.Ioc.Modules
{
    public class NavigationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ViewFactory>()
                .As<IViewFactory>()
                .SingleInstance();

            builder.RegisterType<Navigator>()
                .As<INavigator>()
                .SingleInstance();
        }
    }
}
