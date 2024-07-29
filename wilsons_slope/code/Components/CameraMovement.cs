using Microsoft.VisualBasic;
using Sandbox;

public sealed class CameraMovement : Component
{
	[Property]public PlayerControls player { get; set; }
	[Property]public GameObject bodyRef { get; set; }
	[Property]public GameObject headRef {get; set;}
	[Property]public float distance { get; set; }

	public bool isFirstPerson => distance == 0f;
	Vector3 currentOffset = Vector3.Zero;
	private CameraComponent cameraComponent;
	private ModelRenderer bodyRenderer;

	protected override void OnAwake()
	{
		cameraComponent = Components.Get<CameraComponent>();	
		bodyRenderer = bodyRef.Components.Get<ModelRenderer>();
	}
	protected override void OnUpdate()
	{
		var eyeAngles = headRef.Transform.Rotation.Angles();
		eyeAngles.pitch += Input.MouseDelta.y * .1f;
		eyeAngles.yaw -= Input.MouseDelta.x * .1f;
		eyeAngles.roll = 0f;
		eyeAngles.pitch = eyeAngles.pitch.Clamp(-89.9f, 89.9f);
		headRef.Transform.Rotation = eyeAngles.ToRotation();


		var targetOffset = Vector3.Zero;
		if (player.isCrouching) targetOffset += Vector3.Down * 32f;
		currentOffset = Vector3.Lerp(currentOffset, targetOffset, Time.Delta * 10f);


		if(cameraComponent is not null)
		{
			var camPos = headRef.Transform.Position + currentOffset;
			if(!isFirstPerson)
			{
				var camForawrd = eyeAngles.ToRotation().Forward;
				var camTrace = Scene.Trace.Ray(camPos , camPos - (camForawrd * distance))
					.WithoutTags("player" , "trigger")
					.Run();

				if(camTrace.Hit	)
				{
					camPos = camTrace.HitPosition + camTrace.Normal;
				}
				else
				{
					camPos = camTrace.EndPosition;
				}

				bodyRenderer.RenderType = ModelRenderer.ShadowRenderType.On;
			}
			else
			{
				bodyRenderer.RenderType= ModelRenderer.ShadowRenderType.ShadowsOnly;
			}
			cameraComponent.Transform.Position = camPos;
			cameraComponent.Transform.Rotation = eyeAngles.ToRotation();
		}
	}


}
