using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Randevent : NetworkBehaviour
{
    [SyncVar]
    public float timer;
    [SyncVar]
    public string tstring = "";
    bool triggered = false;
    GameObject unitlist;
    public bool randevents;
    private FactionList fl;
    // Use this for initialization
    void Start()
    {
        unitlist = GameObject.Find("mgrGame");
        fl = unitlist.GetComponent<FactionList>();
    }

    // Update is called once per frame
    void Update()
    {
        if (randevents)
        {
            if (timer >= 0)
            {
                timer -= Time.deltaTime;
                tstring = "" + timer;
            }
            else if (timer < 0 && !triggered)

            {
                Randev();

            }
        }

    }
    void Randev()
    {
        int n = Random.Range(1, 4);

        triggered = true;
        if (n == 1)
        {
            IHAVENOMONEY();

        }
        else if (n == 2)
        {
            Extinction();
        }
        else if (n == 3)
        {
            movealong();
        }

    }
    void IHAVENOMONEY()
    {
        tstring = "No Money!!!";
        GameManager gameManager = Toolbox.RegisterComponent<GameManager>();
        gameManager.money = 0;

    }
    void Extinction()
    {
        tstring = "EXTINCTION!";
        int n = Random.Range(1, 2);
        if (n == 1)
        {
            for (int i = 0; i < fl.CurrentUnitsB.Count; i++)
            {
                Unit a = fl.CurrentUnitsB[i] as Unit;
                //   fl = unitlist.GetComponent<FactionList>();
                NetworkIdentity networkIdentity = a.gameObject.GetComponent<NetworkIdentity>();
                fl.RemoveUnit(networkIdentity.netId);
                // actually works
                NetworkServer.Destroy(a.gameObject);

                // CmdTakeDamage(networkIdentity.netId, 20);
            }

        }
        else if (n == 2)
        {
            for (int i = 0; i < fl.CurrentUnitsR.Count; i++)
            {
                Unit a = fl.CurrentUnitsR[i] as Unit;
                //   fl = unitlist.GetComponent<FactionList>();
                NetworkIdentity networkIdentity = a.gameObject.GetComponent<NetworkIdentity>();
                fl.RemoveUnit(networkIdentity.netId);
                // actually works
                NetworkServer.Destroy(a.gameObject);
            }
        }
    }
    void movealong()
    {
        tstring = "NOTHING";

    }
}


