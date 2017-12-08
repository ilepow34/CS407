using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FactionList : NetworkBehaviour
{
    public ArrayList CurrentUnitsB;
    public ArrayList CurrentUnitsR;
    // Use this for initialization
    [SyncVar]
    public int status = 0;
    [SyncVar]
    public int alen = 0;
    [SyncVar]
    public int blen = 0;

    void Start()
    {
        CurrentUnitsB = new ArrayList();
        CurrentUnitsR = new ArrayList();


        if (!isServer)
        {
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
            Time.timeScale = 0.05f;
            //faction Blue wins
            Debug.Log("Blue Wins!!!");
            status = 1;

        }
        else if (blen <= 0)
        {
            Time.timeScale = 0.05f;
            //Faction red wins 
            Debug.Log("Red Wins!!!");
            status = 2;
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
    public void AddUnit(NetworkInstanceId netId)
    {
        Unit newspawn = NetworkServer.FindLocalObject(netId).GetComponent<Unit>();
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
    public void RpcAddUnit(NetworkInstanceId netId)
    {
        Unit newspawn = NetworkServer.FindLocalObject(netId).GetComponent<Unit>();
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
    public void RpcRemoveUnit(NetworkInstanceId netId)
    {
        Unit unt = NetworkServer.FindLocalObject(netId).GetComponent<Unit>();

        Debug.Log(unt.type);
        if (unt.faction)
        {
            Debug.Log("REM FROM REDRPC");
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
    public void RemoveUnit(NetworkInstanceId netId)
    {
        Unit unt = NetworkServer.FindLocalObject(netId).GetComponent<Unit>();


        Debug.Log(unt.type);
        int prevalen = alen;
        int prevblen = blen;
        if (unt.faction)
        {
            Debug.Log("REM FROM REDS");
            CurrentUnitsR.Remove(unt);
        }
        else
        {
            Debug.Log("REM FROM BLU");
            CurrentUnitsB.Remove(unt);
        }

        alen = CurrentUnitsR.Count;
        blen = CurrentUnitsB.Count;
        if (unt.type.Equals("barracks") && alen == prevalen && alen == 1 && unt.faction)
            alen=0;
        else if (unt.type.Equals("barracks") && blen == prevblen && blen == 1 && !unt.faction)
            blen=0;
        Debug.Log("REM:alen " + alen + " blen" + blen);


    }

}

