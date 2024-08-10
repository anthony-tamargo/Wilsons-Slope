using Sandbox;
using Sandbox.UI.Tests;

public sealed class PlayerStateManager : Component
{
	public enum PLAYER_STATES{
		STARTED,
		IN_PROGRESS,
		FINISH,
		DIED,
	}

	public PLAYER_STATES currentState {private set; get;}

	[Property]
	PlayerTimer playerTimer {get; set;}
	protected override void OnStart()
	{
		ChangePlayerState(PLAYER_STATES.STARTED);
	}
	public void ChangePlayerState(PLAYER_STATES state)
	{
		if (currentState == state) return;

		switch(state)
		{
			case PLAYER_STATES.STARTED:
			playerTimer.RestartTimer();
			break;
			case PLAYER_STATES.IN_PROGRESS:
			playerTimer.StartTimer();
			GameManager.Instance.UpdateGameState(GameState.ROUND_IN_PROGRESS);
			break;
			case PLAYER_STATES.FINISH:
			playerTimer.StopTimer();
			playerTimer.SetPlayerBestTime();
			GameManager.Instance.UpdateGameState(GameState.ROUND_OVER);
			break;
			case PLAYER_STATES.DIED:
			playerTimer.StopTimer();
			GameManager.Instance.UpdateGameState(GameState.ROUND_OVER);
			break;
		}
	}

}
