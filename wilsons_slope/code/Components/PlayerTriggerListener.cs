using System.Diagnostics;
using Sandbox;

public sealed class PlayerTriggerListener : Component, Component.ITriggerListener
{
	



	void ITriggerListener.OnTriggerEnter(Sandbox.Collider other)
	{
		if (other.Components.Get<TriggerObject>() is not null)
		{
			var trigger = other.Components.Get<TriggerObject>();
			Log.Info("Player has entered the " + trigger.triggerType);

			
		}
		
	}
}
