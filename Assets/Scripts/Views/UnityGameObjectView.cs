using strange.extensions.mediation.impl;
using UnityEngine;

namespace net.onur.unitytemplate.view
{
    public class UnityGameObjectView : View
    {
        protected override void Start()
        {
            base.Start();
            
            Debug.Log("Sample View Initalized");
        }
    }
}