using Sandbox;
using Sandbox.UI.Tests;
using System;

public sealed class GameManager : Component
{
	public static GameManager Instance;
	public GameState State;
	public static event Action<GameState> OnGameStateChanged;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		UpdateGameState(GameState.MAIN_MENU);
	}

	public void UpdateGameState(GameState newState)
	{
		if(newState != State)
		{
			State = newState;
			switch(newState)
			{
				case GameState.MAIN_MENU:

				break;

				case GameState.ROUND_START:

				break;

				case GameState.ROUND_IN_PROGRESS:

				break;

				case GameState.ROUND_OVER: 

				break;
			}

			OnGameStateChanged?.Invoke(newState);
		}
	}

}

public enum GameState{
	MAIN_MENU, // when first launching game / quitting out of current round
	ROUND_START, // a new round just started, respawn player if dead, reset time, etc
	ROUND_IN_PROGRESS, // player hasnt died yet, currently working up slope 
	ROUND_OVER, // player either dies or reaches finish line, 
	
}	