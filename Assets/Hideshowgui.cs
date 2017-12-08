using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Hideshowgui : MonoBehaviour
{
    public Text wintxt;
    public GameObject gamegroup;
    GameObject unitlist;
    private int statusg;
    private FactionList fl;
    private bool myfaction;
    public GameObject gomenu;
    // Use this for initialization
    void Start()
    {
        wintxt.gameObject.active = false;
        gomenu.active = false;
        unitlist = GameObject.Find("mgrGame");
        fl = unitlist.GetComponent<FactionList>();
        //find your faction
        myfaction = GameControl.getFaction();

    }

    // Update is called once per frame
   void Update()
    {
        statusg = fl.status;

        if ((statusg == 1 && myfaction == false) || (statusg == 2 && myfaction == true))
        {
            gamegroup.active = false;
            wintxt.gameObject.active = true;
            gomenu.active = true;
            wintxt.text = "Victory!!";
            //StartCoroutine(WaitForIt(5));
            // InvokeRepeating("loadmenu", 5.0f, 15.0f);
        }
        if ((statusg == 1 && myfaction == true) || (statusg == 2 && myfaction == false))
        {
            gamegroup.active = false;
            wintxt.gameObject.active = true;
            gomenu.active = true;
            wintxt.text = "Defeat.";
            // StartCoroutine(WaitForIt(5));
            // InvokeRepeating("loadmenu", 5.0f, 15.0f);
        }


    }
    IEnumerator WaitForIt(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("MainMenu");
    }
    void loadmenu()
    {

        SceneManager.LoadScene("MainMenu");
    }


}
