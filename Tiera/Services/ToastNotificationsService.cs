using Microsoft.Toolkit.Uwp.Notifications;

using Tiera.Contracts.Services;

using Windows.UI.Notifications;

namespace Tiera.Services
{
    public partial class ToastNotificationsService : IToastNotificationsService
    {
        public ToastNotificationsService()
        {
        }

        public void ShowToastNotification(ToastNotification toastNotification)
        {
            ToastNotificationManagerCompat.CreateToastNotifier().Show(toastNotification);
        }
    }
}
