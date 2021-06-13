using Autofac;
using FluToDo.App.Components.Toast;
using System;
using Xamarin.Forms;

namespace FluToDo.App.Ioc.Modules
{
    public class SharedComponentsModule : Module
    {
        
        protected override void Load(ContainerBuilder builder)
        {
            RegisterToast(builder);
        }

        // Get Android or iOS implementation and register it with the interface IToast
        private void RegisterToast(ContainerBuilder builder)
        {
            var toastImplementation = DependencyService.Get<IToast>();
            if (toastImplementation == null)
            {
                throw new InvalidOperationException($"Missing '{typeof(IToast).FullName}' implementation.");
            }
            builder.RegisterInstance(toastImplementation).AsImplementedInterfaces().SingleInstance();
        }
    }
}
