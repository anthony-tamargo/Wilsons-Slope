using System;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using Sandbox;
using Sandbox.Services;

public sealed class PlayerTimer : Component
{
	public float playerCurrentTime { get; private set; }
	public float playerFinishedTime { get; private set; }
	public bool isTimerActive { get; private set; }
	public TimeSince playerTimeSinceRoundStart{ get; private set; }
	

	protected override void OnStart()
	{
		isTimerActive = false; // placeholder for testing?
	}
	


	protected override void OnUpdate()
	{
		if(!isTimerActive)
		{
			playerTimeSinceRoundStart = 0f;	
		}
		else
		{
			playerCurrentTime = playerTimeSinceRoundStart.Relative;
		}

		
		
	}

	public void RestartTimer()
	{
		playerCurrentTime = 0f; 
		
	}
	public void StartTimer()
	{
		isTimerActive = true;
	}
	public void StopTimer()
	{
		isTimerActive = false;
	}
	public float ReturnPlayerTime()
	{
		if(isTimerActive)
		{
			playerCurrentTime = playerTimeSinceRoundStart.Relative;
			return playerCurrentTime;
		}
		else
		{
			playerCurrentTime = playerTimeSinceRoundStart.Absolute;
			return playerCurrentTime;
		}
	}
	public void SetPlayerBestTime()
	{
		if(GameManager.Instance.playerBestTimeReference == 0)
		{
			GameManager.Instance.playerBestTimeReference = playerCurrentTime;
			
			// if player has no best time on record, initialize it to 0
		}

		if(playerCurrentTime < GameManager.Instance.playerBestTimeReference )
		{
			GameManager.Instance.playerBestTimeReference = playerCurrentTime;
			UpdatePlayerStats(playerCurrentTime);
		}
		else
		{
			GameManager.Instance.playerBestTimeReference = GameManager.Instance.playerBestTimeReference;
		}
		
	}
	public float ReturnPlayerBestTime()
	{
		return GameManager.Instance.playerBestTimeReference;
	}

	public void UpdatePlayerStats(float time)
	{
		Stats.SetValue("time",time);

		Stats.Flush();
		
	}





}
