using System.Runtime.InteropServices;
using Sandbox;

public sealed class DeathCam : Component
{
	CameraComponent cam;
	public Angles rotAngles { get; private set;}


	protected override void OnEnabled()
	{
		cam = Components.GetInChildren<CameraComponent>();

		HandleCamMovementInit(); // initializes cam movement vars and rotations
	}

	protected override void OnUpdate()
	{
		HandleCamMovement();
	}

	private void HandleCamMovementInit()
	{
		if(cam is not null)
		{
			var eyeAngles = rotAngles; // our "eyes" while using this camera
			eyeAngles += Input.AnalogLook * 1.5f;
			eyeAngles.roll = 0; // we dont want our camera corkscrewing
			eyeAngles.pitch = eyeAngles.pitch.Clamp(-89.9f, 89.9f); // stops cam from rolling over itself on y axis
			rotAngles = eyeAngles;
		}

	}

	private void HandleCamMovement()
	{
		var eyeAngles = rotAngles; // our "eyes" while using this camera
		eyeAngles += Input.AnalogLook * 1.5f;
		eyeAngles.roll = 0; // we dont want our camera corkscrewing
		eyeAngles.pitch = eyeAngles.pitch.Clamp(-89.9f, 89.9f);// stops cam from rolling over itself on y axis
		rotAngles = eyeAngles;

		var camTrace = Scene.Trace.Ray(cam.Transform.Position, cam.Transform.Position - (eyeAngles.ToRotation().Forward * 300))
			.WithoutTags("player, trigger")
			.Run();
				if(camTrace.Hit	)
				{
					cam.Transform.Position = camTrace.HitPosition + camTrace.Normal;
					Log.Info("" + camTrace.HitPosition);
				}
				else
				{
					cam.Transform.Position = camTrace.EndPosition;
				}
		var lookDirection = rotAngles.ToRotation();
		cam.Transform.Position = Transform.Position + lookDirection.Backward * 300 + Vector3.Up * 75f;
		cam.Transform.Rotation = lookDirection;
	}
}
