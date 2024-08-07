using Sandbox;

public sealed class PlayerHealth : Component
{
	[Property] public float maxHealth { get; set; }
	[Property]public float currentHealth {get; private set;}
	[Property] public GameObject prefabDeathPoint { get; private set;}
	[Property] public GameObject prefabDeathGibs { get; private set;}
	Vector3 currentPlayerPos;
	protected override void OnStart()
	{
		currentHealth = maxHealth;
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
		var dp = prefabDeathPoint.Clone();
		var dpG = prefabDeathGibs.Clone();

		dp.Transform.Position = currentPlayerPos;
		dpG.Transform.Position = currentPlayerPos;
		currentHealth = 0f;
		GameObject.Destroy();
	}



}
