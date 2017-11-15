using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class UIManagerScript : MonoBehaviour {

    public void LoadShit(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
    }
		
    public void CreateGame()
    {
        Toolbox.RegisterComponent<GameStaticData>().isHost = true;
        SceneManager.LoadScene("CreateGame");
    }

    public void JoinGame()
    {
        Toolbox.RegisterComponent<GameStaticData>().isHost = false;
        GameControl.plyrfaction = true;
        SceneManager.LoadScene("FindGame");
    }


    public void Quit()
    {
        Application.Quit(); // why is this a fucking thing if it doesn't do anything?
    }

    public void LoadGame()
    {

    }
}
