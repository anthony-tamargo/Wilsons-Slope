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
	protected override void OnUpdate(){
		Log.Info(currentState);
	}

	public void ChangePlayerState(PLAYER_STATES state)
	{
		if (currentState == state) return;

		switch(state)
		{
			case PLAYER_STATES.STARTED:
			// before player crosses start line
			playerTimer.RestartTimer();
			break;
			case PLAYER_STATES.IN_PROGRESS:
			playerTimer.StartTimer();
			// player crosses start line, walking up slope
			break;
			case PLAYER_STATES.FINISH:
			playerTimer.StopTimer();
			// player crosses finish line
			break;
			case PLAYER_STATES.DIED:
			// player died to a prop
			playerTimer.StopTimer();
			break;
		}
	}

}
