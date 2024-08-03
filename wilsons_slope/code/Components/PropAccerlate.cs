using System;
using System.Net.Http.Headers;
using Sandbox;

public sealed class PropAccerlate : Component
{


	Rigidbody rigidbody { get; set; }
	protected override void OnAwake()
	{
		rigidbody = Components.Get<Rigidbody>();
	}
	protected override void OnStart()
	{
		PropRotation();
	}

	protected override void OnUpdate()
	{
		
		SurfaceAccelerate();
		
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

	private void PropRotation()
	{
		var randRot = new Angles(Game.Random.Float(-360f, 360f),Game.Random.Float(-360f, 360f), Game.Random.Float(-360f, 360f)) ;
		GameObject.Transform.Rotation = randRot;

	}
}
