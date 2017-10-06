using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameController : MonoBehaviour {

    public InputField PlayerName;

	// Use this for initialization
	void Start () {
        if (GameStaticData.PlayerName != "Default") {
            PlayerName.text = GameStaticData.PlayerName;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if (PlayerName.text != "" && PlayerName.text != GameStaticData.PlayerName) {
            GameStaticData.PlayerName = PlayerName.text;
        }
	}
}
