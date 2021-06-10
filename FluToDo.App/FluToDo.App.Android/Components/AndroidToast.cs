using Android.App;
using Android.Widget;
using FluToDo.App.Components.Interfaces;
using FluToDo.App.Droid.Components;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidToast))]
namespace FluToDo.App.Droid.Components
{
    public class AndroidToast : IToast
    {
        public void ShowMessage(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}