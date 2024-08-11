using System.Net.Http;
using System.Runtime.CompilerServices;
using Sandbox;
using Sandbox.Citizen;

public sealed class PlayerControls : Component
{
	[Property] public float groundControl { get; set; } = 4f;
	[Property] public float airControl { get; set; }  = .1f;
	[Property] public float maxForce { get; set; } = 50f;
	[Property] public float speed { get; set; } = 160f;
	[Property] public float runSpeed { get; set; } = 290f;
	[Property] public float crouchSpeed { get; set; } = 90f;
	[Property] public float jumpForce { get; set; } = 400f;

	[Property] public GameObject refHead;
	[Property] public GameObject refBody;

	public Vector3 wishVelocity = Vector3.Zero;
	public bool isCrouching = false;
	public bool isSprinting = false;
	private CharacterController characterController;
	private CitizenAnimationHelper animHelper;

	protected override void OnAwake()
	{
		characterController = Components.Get<CharacterController>();
		animHelper = Components.Get<CitizenAnimationHelper>();
	}
	protected override void OnUpdate()
	{

		UpdateCrouch();
		isSprinting = Input.Down("Run");

		if(Input.Pressed("Jump"))
		{
				Jump();
		}

		RotateBody();
		UpdateAnimation();
	}

	protected override void OnFixedUpdate()
	{
		BuildWishVelocity();
		Move();	
	}
	private void BuildWishVelocity()
	{
		wishVelocity = 0;

		var rotation = refHead.Transform.Rotation;
		if(Input.Down("Forward") ) wishVelocity += rotation.Forward;
		if(Input.Down("Backward") ) wishVelocity += rotation.Backward;
		if(Input.Down("Left") ) wishVelocity += rotation.Left;
		if(Input.Down("Right") ) wishVelocity += rotation.Right;

		wishVelocity = wishVelocity.WithZ(0);
		if(!wishVelocity.IsNearZeroLength) wishVelocity = wishVelocity.Normal;

		if(isCrouching) wishVelocity *= crouchSpeed;
		else if (isSprinting) wishVelocity *= runSpeed;
		else wishVelocity *= speed;
	}
	private void Move()
	{
		var gravity = Scene.PhysicsWorld.Gravity;
		if(characterController.IsOnGround)
		{
			characterController.Velocity = characterController.Velocity.WithZ(0);
			characterController.Accelerate(wishVelocity);
			characterController.ApplyFriction(groundControl);
		}
		else
		{
			characterController.Velocity += gravity * Time.Delta *.5f;
			characterController.Accelerate(wishVelocity.ClampLength(maxForce));
			characterController.ApplyFriction(airControl);
		}

		characterController.Move();
		if(!characterController.IsOnGround)
		{
			characterController.Velocity += gravity * Time.Delta * .5f;
		}
		else
		{
			characterController.Velocity = characterController.Velocity.WithZ(0);
		}
	}

	private void RotateBody()
	{
		if(refBody is not null)
		{
			var targetAngle = new Angles(0 ,refHead.Transform.Rotation.Yaw(), 0).ToRotation();
			float rotateDifference = refBody.Transform.Rotation.Distance(targetAngle);
			if(rotateDifference > 50f || characterController.Velocity.Length > 10f)
			{
				refBody.Transform.Rotation = Rotation.Lerp(refBody.Transform.Rotation , targetAngle, Time.Delta * 2f);
			}
		}
	}
	
	void Jump()
	{
		if(!characterController.IsOnGround) return;
		characterController.Punch(Vector3.Up * jumpForce);
		animHelper?.TriggerJump();
	}	

	void UpdateAnimation()
	{
		if (animHelper is null) return;

		animHelper.WithWishVelocity(wishVelocity);
		animHelper.WithVelocity(characterController.Velocity);
		animHelper.AimAngle = refHead.Transform.Rotation;
		animHelper.IsGrounded = characterController.IsOnGround;
		animHelper.WithLook(refHead.Transform.Rotation.Forward, 1f, .75f, .5f);
		animHelper.MoveStyle = CitizenAnimationHelper.MoveStyles.Run;
		animHelper.DuckLevel = isCrouching ? 1 : 0;
		
	}
	void UpdateCrouch()
	{
		if (characterController is null) return;
		if(Input.Pressed("Duck") && !isCrouching)
		{
			isCrouching = true;
			characterController.Height /= 2f;
		}

		if(Input.Released("Duck") && isCrouching)
		{
			isCrouching = false;
			characterController.Height *= 2f;
		}
	}
}
