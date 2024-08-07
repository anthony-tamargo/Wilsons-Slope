using Microsoft.VisualBasic;
using Sandbox;
using System;
using System.Collections.Generic;
public sealed class PropSpawnPoint : Component
{
	[Property] List<GameObject> propList = new List<GameObject>();
	[Property] List<int> spawnChanceList = new List<int>();
	[Property] List<SpawnData> dataList = new List<SpawnData>();
	[Property] public RealTimeUntil timeUntilSpawn {get;set;}
	[Property]public int totalSpawnProbability {get;private set;}
	private bool isSpawningActive {get;set;}
	
	protected override void OnStart()
	{
		timeUntilSpawn = 5f;
		isSpawningActive = true; // we want to come back to this once we have a gamemanager made and use it to dictate when props can spawn

		InitializeSpawnDataList();
		CalculateTotalSpawnProbability();
		Log.Info(totalSpawnProbability);

		
	}

	protected override void OnUpdate()
	{
		HandlePropSpawn();
	}
	private void HandlePropSpawn()
	{
		if (!isSpawningActive)
		{
			return;
		}
		else
		{
			if (timeUntilSpawn.Relative <= 0f)
			{

				var obj = DecidePropSpawn().Clone();
				obj.Transform.Position = this.Transform.Position;
				timeUntilSpawn = 3f;
				
			}
		}
	}

	private void InitializeSpawnDataList()
	{
		for (int i = 0; i < propList.Count ; i++)
		{
			dataList.Add (new SpawnData (propList[i], spawnChanceList[i]));
		}
		/*
			This is kinda scuffed as it requires the data in propList[i] to properly correspond to spawnChanceList[i].
			It's either this or reading a JsonObject for a variable.
		*/
	}
	private void CalculateTotalSpawnProbability()
	{
		for (int i = 0; i < dataList.Count; i++)
		{
			totalSpawnProbability += dataList[i].spawnWeight;
		}
	}
	private GameObject DecidePropSpawn()
	{
		Random randNum = new Random();
		int randomWeight = randNum.Next(0, totalSpawnProbability+1);
		GameObject propToSpawn;
		int currentWeight = 0;
		
		
		foreach (var data in dataList)
		{
			currentWeight += data.spawnWeight;
				if(randomWeight <= currentWeight)
				{
					propToSpawn = data.propRef;
					return propToSpawn;
				}	

		}


		return null;

	}

	
	
}

public class SpawnData
{
    public GameObject propRef;
    public int spawnWeight;
	public SpawnData( GameObject objRef , int spawnChance)
	{
		propRef = objRef;
		spawnWeight = spawnChance;
	}
    
}
