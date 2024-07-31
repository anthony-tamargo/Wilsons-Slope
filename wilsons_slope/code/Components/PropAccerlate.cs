using System.Net.Http.Headers;
using Sandbox;

public sealed class PropAccerlate : Component
{
	Rigidbody rigidbody { get; set; }
	protected override void OnAwake()
	{
		rigidbody = Components.Get<Rigidbody>();
	}
	protected override void OnStart()
	{
		rigidbody.Velocity = new Vector3 (-25f,0,0);
	}
}
