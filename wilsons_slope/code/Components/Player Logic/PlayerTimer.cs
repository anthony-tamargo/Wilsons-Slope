using System;
using System.Security.Cryptography;
using Sandbox;

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
		if(GameManager.Instance.playerBestTimeReference < playerCurrentTime)
		{
			GameManager.Instance.playerBestTimeReference = playerCurrentTime;
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





}
