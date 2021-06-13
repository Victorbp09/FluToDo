using FluToDo.App.Components.Toast;
using FluToDo.App.iOS.Components;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IosToast))]
namespace FluToDo.App.iOS.Components
{
    public class IosToast : IToast
    {
        const double DELAY = 3;

        NSTimer alertDelay;
        UIAlertController alert;

        public void ShowMessage(string message)
        {
            ShowAlert(message, DELAY);
        }

        private void ShowAlert(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                DismissMessage();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        private void DismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}