using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManagerScript : MonoBehaviour {

	public Text gameNameInput;

	public void LoadGameLobby() {
		GameStaticData.GameName = gameNameInput.text;
		SceneManager.LoadScene ("GameLobby");
	}


    public void LoadShit(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
		


    public void Quit()
    {
        Application.Quit(); // why is this a fucking thing if it doesn't do anything?
    }

    public void LoadGame()
    {

    }
}
