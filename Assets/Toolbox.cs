using UnityEngine;

public class Toolbox : Singleton<Toolbox>
{
	protected Toolbox () {}

	// add public global variable things like managers, things to hold information, etc
	
	// add singletons here?

	void Start()
	{
		GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
		Debug.Log(gameStaticData.GameName);
	}

	void Awake()
	{
		// Do any initializing here, if needed
	}

	static public T RegisterComponent<T>() where T: Component
	{
		return Instance.GetOrAddComponent<T>();
	}	
}
