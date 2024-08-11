using Sandbox;

public sealed class SceneManager : Component
{
	[Property]
	[Description ("List of Scenes we can reference via component")]
	List<SceneFile> gameScenes = new List<SceneFile>();
	public static SceneManager Instance { get; private set;}

	protected override void OnAwake()
	{
		Instance = this;
		GameObject.Flags = GameObjectFlags.DontDestroyOnLoad;
	}

	public SceneFile GetSceneFile (int sceneId)
	{
		return gameScenes[sceneId];
	}
}
