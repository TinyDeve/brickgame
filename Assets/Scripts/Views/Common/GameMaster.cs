using net.onur.brick.views.musiccontroller;
using net.onur.brick.views.soundcontroller;
using strange.extensions.mediation.impl;

public class GameMaster : View {

	public static GameMaster ınstance;

	public SoundController soundController;
	public MusicController musicController;

	protected override void Awake()
	{
		if (ınstance == null)
		{
			ınstance = this;
		} else if (ınstance == this)
		{
			Destroy(gameObject);
		}
            
		DontDestroyOnLoad(gameObject);
	}
}
