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

        // Load components, modules and build the app container with all the dependencies
        public void Run()
        {
            var builder = new ContainerBuilder();
            LoadModules(builder);
            var container = builder.Build();

            var viewFactory = container.Resolve<IViewFactory>();
            RegisterViews(viewFactory);

            ConfigureApplication(container);
        }

        // Load app modules
        private void LoadModules(ContainerBuilder builder)
        {
            builder.RegisterModule<NavigationModule>();
            builder.RegisterModule<ViewsModule>();
            builder.RegisterModule<ServicesModule>();
            builder.RegisterModule<SharedComponentsModule>();
        }

        // Register app viewmodels with their respective pages (views)
        private void RegisterViews(IViewFactory viewFactory)
        {
            viewFactory.Register<ToDoItemsViewModel, ToDoItemsPage>();
            viewFactory.Register<CreateToDoItemViewModel, CreateToDoItemPage>();
        }

        // Configure the application views and set the main page
        private void ConfigureApplication(IContainer container)
        {
            var viewFactory = container.Resolve<IViewFactory>();
            var mainPage = viewFactory.Resolve<ToDoItemsViewModel>();
            var navigationPage = new NavigationPage(mainPage);

            _application.MainPage = navigationPage;
        }
    }
}
