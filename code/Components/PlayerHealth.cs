using Sandbox;

public sealed class PlayerHealth : Component
{
	[Property] public float maxHealth { get; set; }
	float currentHealth;
	protected override void OnStart()
	{
		currentHealth = maxHealth;
	}

	public void Damage(float damage)
	{
		currentHealth -= damage;
		if(currentHealth <= 0f)
		{
			Death();
		}
	}
	private void Death()
	{
		currentHealth = 0f;
		GameObject.Destroy();
	}

}
