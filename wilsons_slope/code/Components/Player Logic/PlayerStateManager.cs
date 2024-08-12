using System;
using System.Runtime.CompilerServices;
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
	public event Action<PlayerState> OnPlayerStateChanged;
	protected override void OnStart()
	{
		ChangePlayerState(PlayerState.STARTED);
		GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;
	}
	protected override void OnDisabled()
	{		
		GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;	
	}

	void GameManager_OnGameStateChanged(GameState state)
	{
		if(state == GameState.ROUND_START)
		{
			ChangePlayerState(PlayerState.STARTED);
		}			
	}
	public void ChangePlayerState(PlayerState state)
	{
		if(currentState  == state)
		{
			return;
		}
		currentState = state;

		switch(state)
		{
			case PlayerState.STARTED:
				HandleStartState();
			break;
			case PlayerState.IN_PROGRESS:
				HandleInProgState();
			break;
			case PlayerState.FINISH:
				HandleFinishState();
			break;
			case PlayerState.DIED:
				HandleDiedState();
			break;
		}

		OnPlayerStateChanged?.Invoke(state);
		Log.Info("Current PlayerState: " + state );
	}

	private void HandleStartState()
	{
		playerTimer.RestartTimer();
	}
	private void HandleInProgState()
	{
		playerTimer.StartTimer();
		GameManager.Instance.UpdateGameState(GameState.ROUND_IN_PROGRESS);
	}
	private void HandleFinishState()
{
		playerTimer.StopTimer();
		playerTimer.SetPlayerBestTime();
		GameManager.Instance.UpdateGameState(GameState.ROUND_OVER);
	}
	private void HandleDiedState()
	{
		playerTimer.StopTimer();
		GameManager.Instance.UpdateGameState(GameState.ROUND_OVER);
	}


}
