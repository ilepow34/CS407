using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNameController : MonoBehaviour {

    public Text GameNameText;
    public InputField GameNameInput;

	// Use this for initialization
	void Start () {
        if (GameNameText != null) {
            GameNameText.text = GameStaticData.GameName;
        }
        if (GameNameInput != null && GameStaticData.GameName != "Default") {
            GameNameInput.text = GameStaticData.GameName;
        }
    }

    void Update() {
        if (GameNameInput != null && GameNameInput.text != "" && GameStaticData.GameName != GameNameInput.text) {
            GameStaticData.GameName = GameNameInput.text;
        }
    }
	
}
