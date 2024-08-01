using Sandbox;

public sealed class TriggerObject : Component
{
	
	public enum TriggerType {
		START,
		FINISH,
		PROP
	}
	[Property] public TriggerType triggerType { get; private set; }

}
