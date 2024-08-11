using System.Dynamic;
using System.Runtime.Intrinsics.Arm;
using System.Security;
using Sandbox;

public sealed class PlayerHealth : Component
{
	public bool isAlive { get; private set;}
	[Property] public GameObject prefabDeathPoint { get; private set;}
	[Property] public GameObject prefabDeathGibs { get; private set;}
	[Property] PlayerStateManager playerStateManager {get; set;}	
	Vector3 currentPlayerPos;
	protected override void OnEnabled()
	{
		playerStateManager.OnPlayerStateChanged += PlayerStateManager_OnPlayerStateChanged;
	}
	protected override void OnDisabled()
	{
		playerStateManager.OnPlayerStateChanged -= PlayerStateManager_OnPlayerStateChanged;
	}

	void PlayerStateManager_OnPlayerStateChanged(PlayerState state)
	{
		if (state == PlayerState.DIED)
		{
			var dp = prefabDeathPoint.Clone();
			var dpG = prefabDeathGibs.Clone();

			dp.Transform.Position = currentPlayerPos;
			dpG.Transform.Position = currentPlayerPos;

			GameObject.Destroy();
		}
	}
	protected override void OnStart()
	{
		isAlive = true;
	}

	protected override void OnUpdate()
	{
		if (GameObject is not null)
		{
			currentPlayerPos = GameObject.Transform.Position;
		}
		else
		{
			return;
		}
	}

	public void Death()
	{
		if(!isAlive)
		{
			return;
			// this check is here in case the collisionlistener triggers this method more than once
		}
		else
		{
			isAlive = false;
			playerStateManager.ChangePlayerState(PlayerState.DIED);
		}
	}




}
