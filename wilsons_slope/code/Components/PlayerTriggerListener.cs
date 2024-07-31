using System.Diagnostics;
using Sandbox;

public sealed class PlayerTriggerListener : Component, Component.ITriggerListener
{
	
	[Property]
	PlayerStateManager playerStateManager {get; set;}


	void ITriggerListener.OnTriggerEnter(Sandbox.Collider other)
	{
		if (other.Components.Get<TriggerObject>() is not null)
		{
			var trigger = other.Components.Get<TriggerObject>();

			if(trigger.triggerType == TriggerObject.TriggerType.START)
			{
				playerStateManager.ChangePlayerState(PlayerStateManager.PLAYER_STATES.IN_PROGRESS);		
				Log.Info("Player has entered the " +  PlayerStateManager.PLAYER_STATES.IN_PROGRESS + " state");
			}
			if(trigger.triggerType == TriggerObject.TriggerType.FINISH)
			{
				playerStateManager.ChangePlayerState(PlayerStateManager.PLAYER_STATES.FINISH);
				Log.Info("Player has entered the " +  PlayerStateManager.PLAYER_STATES.FINISH + " state");
			}
			
		}
		
	}
}
