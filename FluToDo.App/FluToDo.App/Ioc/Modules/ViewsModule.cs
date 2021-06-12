using Autofac;
using FluToDo.App.Pages;
using FluToDo.App.ViewModels;

namespace FluToDo.App.Ioc.Modules
{
    public class ViewsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // viewmodel registration
            builder.RegisterType<ToDoItemsViewModel>();
            builder.RegisterType<CreateToDoItemViewModel>();

            // page registration
            builder.RegisterType<ToDoItemsPage>();
            builder.RegisterType<CreateToDoItemPage>();
        }
    }
}
