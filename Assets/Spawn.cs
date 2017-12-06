using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Spawn : NetworkBehaviour
{
    GameObject unitlist;
    public bool plyrfaction;
    private FactionList fl;
    public GameObject spawnthis;
    private bool runOnce = false;
    // Use this for initialization
    void Start () {
        unitlist = GameObject.Find("mgrGame");
    }
    [Command]
    void CmdSpawnUnit( Quaternion rotation, int connectionId)
    {
        Debug.Log("Running cmd spawn");
        GameObject go = null;

        //instantiate object on server
      
            go = Instantiate(spawnthis, this.gameObject.transform.position, rotation) as GameObject;
       

        if (go == null)
        {
            Debug.Log("Something broke in cmd spawn");
            return;
        }

        // manipulate anything. this was just for testing to see if it syncd properties
        go.GetComponent<Unit>().faction = plyrfaction;
        go.GetComponent<Unit>().type = "SPAWNED FROM SERVER2";
        Debug.Log("Spawninbg2900090909090909090 shit: " + plyrfaction);
        unitlist = GameObject.Find("mgrGame");
        fl = unitlist.GetComponent<FactionList>();
        fl.addUnit(go.GetComponent<Unit>());
        // this then spawns it on clients and sets the owner properly
        NetworkServer.SpawnWithClientAuthority(go, NetworkServer.connections[connectionId]);
    }

    IEnumerator WaitForIt(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);


        Debug.Log("Calling shit?spawnpoint");
        CmdSpawnUnit( Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId);

        //SceneManager.LoadScene("Game");
    }
    // Update is called once per frame
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        if (!runOnce)
        {
            StartCoroutine(WaitForIt(3.0F));
            runOnce = true;
        }

    }
}
