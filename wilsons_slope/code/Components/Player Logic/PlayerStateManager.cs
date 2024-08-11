using System;
using Sandbox;
using Sandbox.UI.Tests;

public enum PlayerState{
		STARTED,
		IN_PROGRESS,
		FINISH,
		DIED,
	}
public sealed class PlayerStateManager : Component
{


	public PlayerState currentState {private set; get;}

	[Property] PlayerTimer playerTimer {get; set;}
	[Property] PlayerHealth playerHealth {get; set;}
	public event Action<PlayerState> OnPlayerStateChanged;
	protected override void OnStart()
	{
		ChangePlayerState(PlayerState.STARTED);
	}
	protected override void OnDisabled()
	{		
	}
	public void ChangePlayerState(PlayerState state)
	{
		if (currentState == state) return;

		switch(state)
		{
			case PlayerState.STARTED:
			playerTimer.RestartTimer();
			break;
			case PlayerState.IN_PROGRESS:
			playerTimer.StartTimer();
			GameManager.Instance.UpdateGameState(GameState.ROUND_IN_PROGRESS);
			break;
			case PlayerState.FINISH:
			playerTimer.StopTimer();
			playerTimer.SetPlayerBestTime();
			GameManager.Instance.UpdateGameState(GameState.ROUND_OVER);
			break;
			case PlayerState.DIED:
			playerTimer.StopTimer();
			GameManager.Instance.UpdateGameState(GameState.ROUND_OVER);
			break;
		}

		OnPlayerStateChanged?.Invoke(state);
		Log.Info("Current PlayerState: " + state );
	}

}
