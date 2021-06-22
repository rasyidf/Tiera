using Windows.UI.Notifications;

namespace Tiera.Contracts.Services
{
    public interface IToastNotificationsService
    {
        public abstract void ShowToastNotification(ToastNotification toastNotification);

        public abstract void ShowToastNotificationSample();
    }
}
