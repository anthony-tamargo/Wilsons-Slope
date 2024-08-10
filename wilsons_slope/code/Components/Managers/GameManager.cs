using Sandbox;
using Sandbox.UI.Tests;
using System;
using System.Reflection;


public enum GameState{
	MAIN_MENU, // when first launching game / quitting out of current round
	ROUND_START, // a new round just started, respawn player if dead, reset time, etc
	ROUND_IN_PROGRESS, // player hasnt died yet, currently working up slope 
	ROUND_OVER, // player either dies or reaches finish line, 
	
}	

public sealed class GameManager : Component
{
	[Property]public SceneFile sceneGame;
	[Property]public SceneFile sceneMainMenu;
	public GameState State{get;private set;}
	public static event Action<GameState> OnGameStateChanged;
	public static GameManager Instance{get; private set;}

	protected override void OnAwake()
	{
		Instance = this;
	}
	public void UpdateGameState(GameState newState)
	{
	
			State = newState;
			switch(newState)
			{
				case GameState.MAIN_MENU:
					HandleMainMenu();
				break;

				case GameState.ROUND_START:
					HandleRoundStart();
				break;

				case GameState.ROUND_IN_PROGRESS:
					HandleRoundInProg();
				break;

				case GameState.ROUND_OVER: 
					HandleRoundOver();
				break;
			}
			
			OnGameStateChanged?.Invoke(newState);
			Log.Info("Current GameState: " + State );
	}

	private void HandleMainMenu()
	{
		Scene.Load(sceneMainMenu);
	}
	private void HandleRoundStart()
	{
		Scene.Load(sceneGame);
	}
	private void HandleRoundInProg()
	{

	}
	private async void HandleRoundOver()
	{
		await Task.Delay(5000);
		UpdateGameState(GameState.ROUND_START);
	}


}
