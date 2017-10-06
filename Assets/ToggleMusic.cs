using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMusic : MonoBehaviour {

    public Toggle musicToggle;

	// Use this for initialization
	void Start () {
        musicToggle.isOn = GameStaticData.musicPlaying;
	}
	
	// Update is called once per frame
	void Update () {
    
        GameStaticData.musicPlaying = musicToggle.isOn;

        if (musicToggle.isOn && !GameStaticData.sound.isPlaying) {
            GameStaticData.sound.Play();
        } else if (!musicToggle.isOn && GameStaticData.sound.isPlaying) {
            GameStaticData.sound.Stop();
        }
	}
}
