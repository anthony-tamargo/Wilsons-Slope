using Sandbox;
using Sandbox.UI;

public sealed class PlayerCollisionListener : Component, Component.ICollisionListener
{
	[Property]
	PlayerHealth playerHealth {get; set;}

	void ICollisionListener.OnCollisionStart(Sandbox.Collision other)
	{
		if(other.Other.GameObject.Components.Get<CollisionObject>() is not null)
		{
			var colRef = other.Other.GameObject.Components.Get<CollisionObject>();
			if(colRef.colType == CollisionObject.CollisionType.PROP)
			{
				playerHealth.Death();
				return;
			}
			else
			{
				return;
			}
		}
	}
}
