using System.Dynamic;
using System.Security;
using Sandbox;

public sealed class PlayerHealth : Component
{
	[Property] public float maxHealth { get; set; }
	[Property]public float currentHealth {get; private set;}
	public bool isAlive { get; private set;}
	[Property] public GameObject prefabDeathPoint { get; private set;}
	[Property] public GameObject prefabDeathGibs { get; private set;}
	[Property] PlayerStateManager playerStateManager {get; set;}	
	Vector3 currentPlayerPos;
	protected override void OnStart()
	{
		currentHealth = maxHealth;
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
	public void Damage(float damage)
	{
		currentHealth -= damage;
		if(currentHealth <= 0f)
		{
			Death();
		}
	}
	public void Death()
	{
		if(currentHealth == 0f)
		{
			return;
			// this check is here in case the collisionlistener triggers this method more than once
		}
		else
		{
			var dp = prefabDeathPoint.Clone();
			var dpG = prefabDeathGibs.Clone();

			dp.Transform.Position = currentPlayerPos;
			dpG.Transform.Position = currentPlayerPos;

			currentHealth = 0f;
			isAlive = false;
			playerStateManager.ChangePlayerState(PlayerStateManager.PLAYER_STATES.DIED);
			GameObject.Destroy();
	}
		}




}
