using net.onur.unitytemplate.service.localpush;
using net.onur.unitytemplate.service.onesignal;
using strange.extensions.command.impl;
using UnityEngine;

namespace net.onur.unitytemplate.commands
{
    public class StartAppCommand : Command
    {        
        [Inject] 
        public IOneSignalService OneSignalService { get; set; }
        
        [Inject] 
        public ILocalPushService LocalPushService { get; set; }
        
        public override void Execute()
        {
            ConfigureApp();
        }

        private void ConfigureApp()
        {
            Application.targetFrameRate = 60;   
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
           
        }
    }
}