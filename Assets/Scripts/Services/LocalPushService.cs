using System;
using UTNotifications;

namespace net.onur.unitytemplate.service.localpush
{
    public class LocalPushService : ILocalPushService
    {
        public void Init()
        {
            var initialized = Manager.Instance.Initialize(true);
            if (!initialized) return;
            
            Manager.Instance.SetBadge(0);
            Manager.Instance.CancelAllNotifications();   
            
        }

        public void CancelNotification(int id)
        {
            if (!Manager.Instance.Initialized) return;
            
            Manager.Instance.CancelNotification(id);
        }
        
        public void ScheduleLocalNotification(DateTime triggerDateTime, string title, string message, int id)
        {
            if (!Manager.Instance.Initialized)
            {
                return;
            }
            
            Manager.Instance.CancelNotification(id);
            Manager.Instance.ScheduleNotification(triggerDateTime, title, message, id);
        }
    }
}