using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenResolutions : MonoBehaviour {

    Resolution[] resolutions;
    public Dropdown dropdownMenu;

	// Use this for initialization
	void Start () {
		resolutions = Screen.resolutions;

        int curIndex = -1;

        dropdownMenu.ClearOptions();
        dropdownMenu.options.Clear();

        Debug.Log(Screen.resolutions.Length); // the fuck?

        dropdownMenu.options.Add(new Dropdown.OptionData("800x600"));
        dropdownMenu.options.Add(new Dropdown.OptionData("1920x1080"));
        GameStaticData gameStaticData = Toolbox.RegisterComponent<GameStaticData>();
        dropdownMenu.onValueChanged.AddListener(delegate {
            if (dropdownMenu.value == 0) {
                Screen.SetResolution(800, 600, true);
                gameStaticData.currentRes = new int[] { 800, 600};
            } else {
                Screen.SetResolution(1920, 1080, true);
                gameStaticData.currentRes = new int[] { 1920, 1080};
            }
        });

        if (gameStaticData.currentRes[0] == 1920) {
            dropdownMenu.value = 1;
        }

        for (int i = 0; i < resolutions.Length; i++) {
            dropdownMenu.options.Add(new Dropdown.OptionData(ResToString(resolutions[i])));
            /*
            dropdownMenu.onValueChanged.AddListener(delegate {
                Screen.SetResolution(resolutions[dropdownMenu.value].width, resolutions[dropdownMenu.value].height, true);
                GameStaticData.currentRes = resolutions[dropdownMenu.value];
            });
            if (curIndex == -1 && resolutions[i].width == GameStaticData.currentRes.width && resolutions[i].height == GameStaticData.currentRes.height) {
                curIndex = i;
            }
            */

        }

        dropdownMenu.RefreshShownValue();

        if (curIndex != -1) {
            dropdownMenu.value = curIndex;
        }


	}
	
	// Update is called once per frame
	void Update () {
		
	}

    string ResToString(Resolution res) {
        return res.width + " x " + res.height;
    }
}
