using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameNameController : MonoBehaviour {

    public Text GameName;

	// Use this for initialization
	void Start () {
        GameName.text = GameStaticData.GameName;
    }
	
}
