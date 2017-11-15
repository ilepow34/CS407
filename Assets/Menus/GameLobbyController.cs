using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobbyController : MonoBehaviour {

    public Text GameName;

	// Use this for initialization
	void Start () {
		GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
        if (gameStaticData != null)
        {
        	GameName.text = gameStaticData.GameName;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
