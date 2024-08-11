using Sandbox;

public sealed class PlayerDeathGibs : Component
{
	protected override void OnEnabled()
	{
		GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
	}
	protected override void OnDisabled()
	{
		GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;
	}

	void GameManager_OnGameStateChanged(GameState state)
	{
		if (state == GameState.ROUND_START)
		{
			GameObject.Destroy();
		}
	}

}
