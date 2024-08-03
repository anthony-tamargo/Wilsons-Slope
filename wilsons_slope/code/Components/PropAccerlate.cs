using System;
using System.Net.Http.Headers;
using Sandbox;

public sealed class PropAccerlate : Component
{
	public enum PROP_ACCERATION_TYPE{
		ARIEL, // prop will spawn in flying forward
		SURFACE, // prop will drop to slope before accelerating
	}
	[Property]
	public PROP_ACCERATION_TYPE propAccelType { get; set; }

	Rigidbody rigidbody { get; set; }
	protected override void OnAwake()
	{
		rigidbody = Components.Get<Rigidbody>();
	}
	protected override void OnStart()
	{
	}

	protected override void OnUpdate()
	{
		AccelerateProp();
		
	}

	private void AccelerateProp()
	{
		switch(propAccelType)
		{
			case PROP_ACCERATION_TYPE.SURFACE:
				SurfaceAccelerate();
			break;

			case PROP_ACCERATION_TYPE.ARIEL:
				ArielAccelerate();
			break;
		}
	}

	private void SurfaceAccelerate()
	{
		if(SurfaceGroundCheck())
		{
			rigidbody.Velocity = new Vector3(-750f, rigidbody.Velocity.y,rigidbody.Velocity.z);
		}
	}
	private bool SurfaceGroundCheck()
	{
		var propPos = GameObject.Transform.Position;
		var groundCheckRay = Scene.Trace
			.Ray(propPos , propPos + (Vector3.Down * 15f) )
			.WithTag("solid")
			.Run();
			if(groundCheckRay.Hit)
			{
				return true;
			}
			else
			{
				return false;
			}

	}
	private void ArielAccelerate()
	{

	}
	private void PropRotationLerp()
	{
		var randRot = new Angles(Game.Random.Float(-360f, 360f),Game.Random.Float(-360f, 360f), Game.Random.Float(-360f, 360f)) ;
		var targetRot = randRot;
		GameObject.Transform.Rotation = Angles.Lerp(GameObject.Transform.Rotation, targetRot , 5 * Time.Delta);

	}
}
