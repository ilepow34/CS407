using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FactionList : NetworkBehaviour
{
    public static ArrayList CurrentUnitsB;
    public static ArrayList CurrentUnitsR;
    // Use this for initialization

    public int alen;
    public int blen;

    void Start()
    {
        CurrentUnitsB = new ArrayList();
        CurrentUnitsR = new ArrayList();
        //InvokeRepeating("Update", 1f, 1f);
    }

    void checkWin()
    {
        Debug.Log("Checkingisit?? " + "alen " + alen + " blen" + blen);
        if (CurrentUnitsB.Count <= 0)
        {
            // Time.timeScale = 0.1f;
            //faction Blue wins
            Debug.Log("Red Wins!!!");

        }
        else if (CurrentUnitsR.Count <= 0)
        {
            //Time.timeScale = 0.1f;
            //Faction red wins 
            Debug.Log("Blue Wins!!!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            return;
        }
        InvokeRepeating("checkWin", 15.0f, 15.0f);
        //yield return new WaitForSeconds(15.0f);
        // Thread.Sleep(15000);

        // System.Threading.Thread.Sleep(
        //(int)System.TimeSpan.FromSeconds(15).TotalMilliseconds);

    }
    public void addUnit(Unit newspawn)
    {
        if (newspawn.faction)
        {
            CurrentUnitsR.Add(newspawn);
        }
        else CurrentUnitsB.Add(newspawn);
        alen = CurrentUnitsR.Count;
        blen = CurrentUnitsB.Count;
        Debug.Log("alen " + alen + " blen" + blen);
    }
    public void removeUnit(Unit unt)
    {

        Debug.Log(unt.type);
        if (unt.faction)
        {
            Debug.Log("REM FROM RED");
            CurrentUnitsR.Remove(unt);
        }
        else
        {
            Debug.Log("REM FROM BLU");
            CurrentUnitsB.Remove(unt);
        }

        alen = CurrentUnitsR.Count;
        blen = CurrentUnitsB.Count;
        Debug.Log("REM:alen " + alen + " blen" + blen);

      
    }

}

