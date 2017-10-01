using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIManagerScript : MonoBehaviour {

    public void LoadTest1()
    {
        SceneManager.LoadScene("Test1");
    }

    public void LoadTest2()
    {
        SceneManager.LoadScene("Test2");
    }
}
