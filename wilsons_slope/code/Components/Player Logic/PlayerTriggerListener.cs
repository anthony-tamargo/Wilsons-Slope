using System.Diagnostics;
using Sandbox;

public sealed class PlayerTriggerListener : Component, Component.ITriggerListener
{
	
	[Property]
	PlayerStateManager playerStateManager {get; set;}
	[Property]
	PlayerHealth playerHealth {get; set;}	


	void ITriggerListener.OnTriggerEnter(Sandbox.Collider other)
	{
		if (other.Components.Get<TriggerObject>() is not null)
		{
			var trigger = other.Components.Get<TriggerObject>();

			if(trigger.triggerType == TriggerObject.TriggerType.START)
			{
				playerStateManager.ChangePlayerState(PlayerStateManager.PLAYER_STATES.IN_PROGRESS);		
			}
			if(trigger.triggerType == TriggerObject.TriggerType.FINISH)
			{
				playerStateManager.ChangePlayerState(PlayerStateManager.PLAYER_STATES.FINISH);
			}
			if(trigger.triggerType == TriggerObject.TriggerType.PROP)
			{
				playerHealth.Death();
			}
			else
			{
				return; 
			}
			
		}
		
	}
}
