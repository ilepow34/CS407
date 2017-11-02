using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameController : MonoBehaviour {

    public InputField PlayerName;

	// Use this for initialization
	void Start ()
	{
	    GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
            if (gameStaticData.PlayerName != "Default") {
            	PlayerName.text = gameStaticData.PlayerName;
       	    }
	}
	
	// Update is called once per frame
	void Update () 
	{
	    GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
	    if (PlayerName.text != "" && PlayerName.text != gameStaticData.PlayerName)
	    {
                gameStaticData.PlayerName = PlayerName.text;
            }
	}
}
