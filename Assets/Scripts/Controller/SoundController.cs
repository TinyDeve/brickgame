using net.onur.brick.models.settings;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace net.onur.brick.views.soundcontroller
{
	public class SoundController : View
	{
		[Inject] 
		public SettingsModel SettingsModel { get; set; }
        
		public AudioSource audioSource;
		public enum SoundType { CLİCK, FAİL, TAP}

		//[HideInInspector]
		[SerializeField]private AudioClip[] buttonClips;

		public void PlaySound(SoundType type = SoundType.CLİCK)
		{
			if (!SettingsModel.Sound) return;
            
			var index = (int)type;
            
			audioSource.PlayOneShot(buttonClips[index]);
		}
	}
}
