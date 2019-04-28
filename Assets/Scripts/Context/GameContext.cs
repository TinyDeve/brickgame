using net.onur.brick.models.game;
using net.onur.brick.models.settings;
using net.onur.unitytemplate.commands;
using net.onur.unitytemplate.service.localpush;
using net.onur.unitytemplate.service.onesignal;
using net.onur.unitytemplate.service.task;
using net.onur.unitytemplate.signals;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

public class GameContext : MVCSContext
{
	public GameContext (MonoBehaviour view) : base (view)
	{
	}
	
	public override IContext Start ()
	{
		base.Start ();
		var startSignal = injectionBinder.GetInstance<StartAppSignal> ();
		startSignal.Dispatch ();
		return this;
	}
	
	protected override void addCoreComponents ()
	{
		base.addCoreComponents ();
		injectionBinder.Unbind<ICommandBinder> ();
		injectionBinder.Bind<ICommandBinder> ().To<SignalCommandBinder> ().ToSingleton ();
	}

	protected override void mapBindings()
	{
		base.mapBindings();

		//signals to command
		commandBinder.Bind<StartAppSignal> ().To<StartAppCommand> ().Once ();
		injectionBinder.Bind<TapSignal> ().ToSingleton ();
		
		//services
		injectionBinder.Bind<TaskService> ().ToSingleton ();
		injectionBinder.Bind<IOneSignalService>().To<OneSignalService>().ToSingleton();
		injectionBinder.Bind<ILocalPushService>().To<LocalPushService>().ToSingleton();
		
		//models
		injectionBinder.Bind<SettingsModel> ().ToSingleton ();
		injectionBinder.Bind<GameModel> ().ToSingleton ();
	}
}