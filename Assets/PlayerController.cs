using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Text PlayerName;

	// Use this for initialization
	void Start () {
		PlayerName.text = GameStaticData.PlayerName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
