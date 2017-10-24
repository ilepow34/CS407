using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    public GameObject music;
    public AudioSource sound;

    void Awake() {
        music = GameObject.Find("MUSIC");
        if (music == null) {
            music = this.gameObject;
            music.name = "MUSIC";
            DontDestroyOnLoad(music);
	    GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
            gameStaticData.sound = this.sound;
        } else {
            if (this.gameObject.name != "MUSIC") {
                Destroy(this.gameObject);
            }
        }
    }

}
