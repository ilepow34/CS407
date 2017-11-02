using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStaticData : MonoBehaviour {

    public int[] currentRes = new int[] { Screen.resolutions[0].width, Screen.resolutions[0].height };
    public string GameName = "Default";
    public AudioSource sound;
    public bool musicPlaying = false;
    public string PlayerName = "Default";
}
