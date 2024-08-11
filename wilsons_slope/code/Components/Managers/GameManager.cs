using Sandbox;
using Sandbox.UI.Tests;
using System;
using System.Reflection;


public enum GameState{
	ROUND_START, // a new round just started, respawn player if dead, reset time, etc
	ROUND_IN_PROGRESS, // player hasnt died yet, currently working up slope 
	ROUND_OVER, // player either dies or reaches finish line, 
	
}	

public sealed class GameManager : Component
{
	[Property] public GameObject playerPrefab {get; private set;}
	[Property] public GameObject spawnPointParent {get; private set;}
	[Property] List<Transform> spawnPoints = new List<Transform>();
	public GameState State{get;private set;}
	public static event Action<GameState> OnGameStateChanged;
	public static GameManager Instance{get; private set;}

	protected override void OnAwake()
	{
		Instance = this;
	}
	protected override void OnStart()
	{
		InitializeSpawnPoints();
		UpdateGameState(GameState.ROUND_START);
	}
	private void InitializeSpawnPoints()
	{
		for (int i = 0; i < spawnPointParent.Children.Count; i++)
		{
			spawnPoints.Add(spawnPointParent.Children[i].Transform.World);
		}
	}
	private int ReturnRandomSpawn()
	{
		Random randInt = new Random();
		int spawnPos = randInt.Next(0, spawnPoints.Count );
		return spawnPos;
	}

	public void UpdateGameState(GameState newState)
	{
	
			State = newState;
			switch(newState)
			{
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


	private void HandleRoundStart()
	{
		var player = playerPrefab.Clone();
		player.Transform.Position = spawnPoints[ReturnRandomSpawn()].Position;

		
	}
	private void HandleRoundInProg()
	{

	}
	private async void HandleRoundOver()
	{
		await Task.DelayRealtimeSeconds(5);
		UpdateGameState(GameState.ROUND_START);

	}
		


}
