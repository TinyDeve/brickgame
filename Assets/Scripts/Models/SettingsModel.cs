using UnityEngine;

namespace net.onur.brick.models.settings
{
    public class SettingsModel
    {
        private const string KeySettingsSound = "settings_sound";
        private const string KeySettingsMusic = "settings_music";
       
        public bool Music {
            get {
                return GameUtils.GetBool(KeySettingsMusic, true);
            }
            set {
                GameUtils.SetBool(KeySettingsMusic, value);
            }
        }
        
        public bool Sound {
            get {
                return GameUtils.GetBool(KeySettingsSound, true);
            }
            set {
                GameUtils.SetBool(KeySettingsSound, value);
            }
        }
       
        public void Save()
        {
            PlayerPrefs.Save();
        }
    }
}