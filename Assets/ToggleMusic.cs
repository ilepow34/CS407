using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour {

    public Toggle musicToggle;

	// Use this for initialization
	void Start ()
	{
		GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
        	musicToggle.isOn = gameStaticData.musicPlaying;
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
        	gameStaticData.musicPlaying = musicToggle.isOn;
        	if (musicToggle.isOn && !gameStaticData.sound.isPlaying) {
            		gameStaticData.sound.Play();
        	} else if (!musicToggle.isOn && gameStaticData.sound.isPlaying) {
            		gameStaticData.sound.Stop();
        	}
	}
}
