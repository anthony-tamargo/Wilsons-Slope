using System;
using Sandbox;

public sealed class CollisionObject : Component
{
	public enum CollisionType
	{
		PROP,

	}
	[Property] public CollisionType colType { get; private set; }
}
