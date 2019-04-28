namespace net.onur.unitytemplate.service.onesignal
{
    public class OneSignalService : IOneSignalService
    {
        public void Init()
        {
            OneSignal.SetLogLevel(OneSignal.LOG_LEVEL.VERBOSE, OneSignal.LOG_LEVEL.NONE);
            
            OneSignal.StartInit("one-signal-id")
                .HandleNotificationReceived(HandleNotificationReceived)
                .HandleNotificationOpened(HandleNotificationOpened)
                .EndInit();
      
            OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
            OneSignal.permissionObserver += OneSignal_permissionObserver;
            OneSignal.subscriptionObserver += OneSignal_subscriptionObserver;
            OneSignal.emailSubscriptionObserver += OneSignal_emailSubscriptionObserver;
        }
        
        private void HandleNotificationReceived(OSNotification notification) {

        }
   
        private void HandleNotificationOpened(OSNotificationOpenedResult result) {

 
        }
        
        private void OneSignal_subscriptionObserver(OSSubscriptionStateChanges stateChanges) {
           
        }

        private void OneSignal_permissionObserver(OSPermissionStateChanges stateChanges) {
           
        }

        private void OneSignal_emailSubscriptionObserver(OSEmailSubscriptionStateChanges stateChanges) {
           
        }
    }
}