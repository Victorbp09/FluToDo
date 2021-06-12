using Autofac;
using Autofac.Core;
using FluToDo.App.Helpers;
using FluToDo.Service.Http;
using FluToDo.Service.Http.Interfaces;
using System.Collections.Generic;

namespace FluToDo.App.Ioc.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ToDoItemsService>()
                .As<IToDoItemsService>()
                .SingleInstance()
                .WithParameters(new List<Parameter>()
            {
                new NamedParameter("baseUrl", Constants.BASE_URL)
            });
        }
    }
}
