using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FactionList : NetworkBehaviour
{
    [SyncVar]
    public static ArrayList CurrentUnitsB;
    [SyncVar]
    public static ArrayList CurrentUnitsR;
    // Use this for initialization

    [SyncVar]
    public int alen = 0;
    [SyncVar]
    public int blen = 0;

    void Start()
    {
        CurrentUnitsB = new ArrayList();
        CurrentUnitsR = new ArrayList();
		
		
		if (!isServer) {
			return;
		}
		InvokeRepeating("checkWin", 15.0f, 15.0f);
        //InvokeRepeating("Update", 1f, 1f);
    }
	
	
    void checkWin()
    {
        Debug.Log("Checkingisit?? " + "alen " + alen + " blen " + blen);
        if (alen <= 0)
        {
            // Time.timeScale = 0.1f;
            //faction Blue wins
            Debug.Log("Blue Wins!!!");

        }
        else if (blen <= 0)
        {
            //Time.timeScale = 0.1f;
            //Faction red wins 
            Debug.Log("Red Wins!!!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!isServer)
        {
            return;
        }
        //yield return new WaitForSeconds(15.0f);
        // Thread.Sleep(15000);

        // System.Threading.Thread.Sleep(
        //(int)System.TimeSpan.FromSeconds(15).TotalMilliseconds);

    }
    //[Command]
    public void AddUnit(Unit newspawn)
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

    [ClientRpc]
    public void RpcAddUnit(Unit newspawn)
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
    [ClientRpc]
    public void RpcRemoveUnit(Unit unt)
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
   // [Command]
    public void RemoveUnit(Unit unt)
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

