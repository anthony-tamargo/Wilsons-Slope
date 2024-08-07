using Sandbox;

public sealed class PropTriggerListener : Component , Component.ITriggerListener
{

	void ITriggerListener.OnTriggerEnter(Sandbox.Collider other)
	{
		if (other.Components.Get<TriggerObject>() is not null)
		{
			var trigger = other.Components.Get<TriggerObject>();
			if(trigger.triggerType == TriggerObject.TriggerType.DESTROY)
			{	
				Log.Info(GameObject.Parent.Name + " entered " + trigger.triggerType);
				GameObject.Parent.Destroy();
			}
			else
			{
				Log.Info(GameObject.Parent.Name + " entered " + trigger.triggerType + ",not a Destory Trigger");
			
			}

		}
		else
		{
			Log.Info(GameObject.Parent.Name + " couldn't find TriggerObject component");
		}

	}

}
