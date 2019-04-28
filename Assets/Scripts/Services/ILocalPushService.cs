using System;

namespace net.onur.unitytemplate.service.localpush
{
    public interface ILocalPushService
    {
        void Init();
        void ScheduleLocalNotification(DateTime triggerDateTime, string title, string message, int id);
        void CancelNotification(int id);
    }
}