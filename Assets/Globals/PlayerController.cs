using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Text PlayerName;

	// Use this for initialization
	void Start () {
		GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
		PlayerName.text = gameStaticData.PlayerName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
