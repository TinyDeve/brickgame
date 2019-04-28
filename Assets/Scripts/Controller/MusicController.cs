using System.Collections;
using System.Collections.Generic;
using net.onur.brick.models.settings;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace net.onur.brick.views.musiccontroller
{
	public class MusicController : View {

		[Inject] 
		public SettingsModel SettingsModel { get; set; }
        
		
		[SerializeField]private AudioSource audioSource;


		protected override void Start()
		{
			base.Start();
			audioSource.mute = SettingsModel.Music;
		}
	}
}
