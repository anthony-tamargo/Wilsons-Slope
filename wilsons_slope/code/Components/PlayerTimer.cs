using Sandbox;

public sealed class PlayerTimer : Component
{
	public TimeSince playerRoundTime {get; private set;} // value used to see how long it took player to get to top
	public float playerFinalRoundTime {get; private set;} // value used to see the player's time spent after they finished
	public float playerCurrentRoundTime{get; private set;} 

	[Property] public PlayerStateManager playerStateManager {get; private set;}
	


	protected override void OnUpdate()
	{
		while (playerStateManager.currentState == PlayerStateManager.PLAYER_STATES.STARTED)
		{
			if (playerRoundTime > 0f)
			{
				playerRoundTime = 0f;
			}
		}
	}
	public void StartTimer()
	{
		playerCurrentRoundTime = playerRoundTime.Relative;
		
	}	
	public void FinishTimer()
	{
		playerRoundTime = 0f;
		playerCurrentRoundTime = playerRoundTime.Absolute;

	}

}
