using UnityEngine;
using UnityEngine.UI;

namespace net.onur.brick.view.button
{
    public class ToggleButton : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _iconOn, _iconOff;
        
        public void SetActive(bool active)
        {
            _image.sprite = active ? _iconOn : _iconOff;            
        }
    }
    
    
}