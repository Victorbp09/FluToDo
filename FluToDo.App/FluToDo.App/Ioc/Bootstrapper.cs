using Autofac;
using FluToDo.App.Components.Navigation;
using FluToDo.App.Ioc.Modules;
using FluToDo.App.Pages;
using FluToDo.App.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FluToDo.App.Ioc
{
    public class Bootstrapper
    {
        private readonly App _application;

        public Bootstrapper(App application)
        {
            _application = application;
        }

        public void Run()
        {
            var builder = new ContainerBuilder();
            LoadModules(builder);
            var container = builder.Build();

            var viewFactory = container.Resolve<IViewFactory>();
            RegisterViews(viewFactory);

            ConfigureApplication(container);
        }

        private void LoadModules(ContainerBuilder builder)
        {
            builder.RegisterModule<NavigationModule>();
            builder.RegisterModule<ViewsModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<SharedComponentsModule>();
        }

        private void RegisterViews(IViewFactory viewFactory)
        {
            viewFactory.Register<ToDoItemsViewModel, ToDoItemsPage>();
            viewFactory.Register<CreateToDoItemViewModel, CreateToDoItemPage>();
        }

        private void ConfigureApplication(IContainer container)
        {
            // set main page
            var viewFactory = container.Resolve<IViewFactory>();
            var mainPage = viewFactory.Resolve<ToDoItemsViewModel>();
            var navigationPage = new NavigationPage(mainPage);

            _application.MainPage = navigationPage;
        }
    }
}
