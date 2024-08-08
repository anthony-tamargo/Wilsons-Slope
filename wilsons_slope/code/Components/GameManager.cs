using Sandbox;
using Sandbox.UI.Tests;
using System;


public enum GameState{
	MAIN_MENU, // when first launching game / quitting out of current round
	ROUND_START, // a new round just started, respawn player if dead, reset time, etc
	ROUND_IN_PROGRESS, // player hasnt died yet, currently working up slope 
	ROUND_OVER, // player either dies or reaches finish line, 
	
}	

public sealed class GameManager : Component
{
	[Property]public SceneFile gameSceneRef;
	[Property]public SceneFile menuSceneRef;
	public GameState State{get;private set;}
	public static event Action<GameState> OnGameStateChanged;
	private static GameManager _instance;
	public static GameManager Instance 
	{
		get
		{
			if(_instance == null)
			{
				var curScene = Game.ActiveScene;	
				_instance = curScene.Components.Get<GameManager>(); // finds any components of type GameManger in current scene
				{
					if(_instance == null)
					{
						Log.Info("GameManager instance not found in the scene.");
					}
				}
				
			}
			return _instance;
		}

		
	}


	protected override void OnAwake()
	{
		
		if(_instance !=null && _instance != this)
		{
			this.Destroy();
		}
		else
		{
			_instance = this;
			GameObject.Flags = GameObjectFlags.DontDestroyOnLoad;
		}
		
	}


	protected override void OnStart()
	{
		
		
		
		
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

				break;

				case GameState.ROUND_OVER: 

				break;
			}
			Log.Info("Current GameState: " + State );
			OnGameStateChanged?.Invoke(newState);
		
	}

	private void HandleMainMenu()
	{
		Scene.Load(menuSceneRef);
	}
	private void HandleRoundStart()
	{
		Scene.Load(gameSceneRef);
	}


}
