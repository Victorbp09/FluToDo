using Xamarin.Forms;
using Autofac;

namespace FluToDo.App.Core
{
    public abstract class FluToDoApplication : Application
    {
        public IContainer Container { get; set; }
    }
}
