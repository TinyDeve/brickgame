using net.onur.brick.views.soundcontroller;
using strange.extensions.mediation.impl;
using UnityEngine;
using UnityEngine.UI;

namespace net.onur.brick.view.button
{
    [RequireComponent(typeof(Button))]
    public class BaseButton : View
    {
        [SerializeField]
        private SoundController.SoundType _soundType;
        
        protected Button Button;

        protected override void Start()
        {
            Button = GetComponent<Button>();
            if (Button != null)
            {
                Button.onClick.AddListener(OnButtonClick);
            }
            base.Start();
        }
        
        public virtual void OnButtonClick()
        {
            GameMaster.ınstance.soundController.PlaySound(_soundType); 
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            if (Button != null)
            {
                Button.onClick.RemoveListener(OnButtonClick);
            }
        }
    }
}