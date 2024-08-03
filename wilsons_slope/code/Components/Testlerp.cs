using Sandbox;

public sealed class Testlerp : Component
{

	protected override void OnStart()
	{
		
	}
	protected override void OnUpdate()
	{
		PropRotationLerp();
	}
		private void PropRotationLerp()
	{
		var randRot = new Angles(Game.Random.Float(-360f, 360f),Game.Random.Float(-360f, 360f), Game.Random.Float(-360f, 360f)) ;
		var targetRot = randRot;
		GameObject.Transform.Rotation = Angles.Lerp(GameObject.Transform.Rotation, targetRot , 10 * Time.Delta);

	}
}
