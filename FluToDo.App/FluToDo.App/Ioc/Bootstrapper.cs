using Autofac;
using Autofac.Core;
using System.Collections.Generic;
using FluToDo.Service.Http.Interfaces;
using FluToDo.Service.Http;
using FluToDo.App.Helpers;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using FluToDo.App.ViewModels;

namespace FluToDo.App.Ioc
{
    public sealed class Bootstrapper
    {
        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            RegisterHttpServices(builder);
            RegisterViewModels(builder);
            IContainer container = builder.Build();

            AutofacServiceLocator autofacServiceLocator = new AutofacServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => autofacServiceLocator);
        }
        private static void RegisterHttpServices(ContainerBuilder builder)
        {
            builder.RegisterType<ToDoItemsService>()
                .As<IToDoItemsService>()
                .SingleInstance()
                .WithParameters(new List<Parameter>()
            {
                new NamedParameter("baseUrl", Constants.BASE_URL)
            });
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<ToDoItemsViewModel>().AsSelf();
        }
    }
}
