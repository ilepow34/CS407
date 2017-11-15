using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNameController : MonoBehaviour {

    public Text GameNameText;
    public InputField GameNameInput;

	// Use this for initialization
	void Start () {
        GameObject.Find("LobbyManager"); // initialize it?

    	GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();

        if (GameNameText != null) {
            GameNameText.text = gameStaticData.GameName;
        }
        if (GameNameInput != null && gameStaticData.GameName != "Default") {
            GameNameInput.text = gameStaticData.GameName;
        }
    }

    void Update() {
        if (GameNameInput == null) {
            return;
        }
	GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
        if (GameNameInput != null && GameNameInput.text != "" && gameStaticData.GameName != GameNameInput.text) {
            gameStaticData.GameName = GameNameInput.text;
        }
    }
	
}
