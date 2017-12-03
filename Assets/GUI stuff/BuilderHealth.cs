using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuilderHealth : NetworkBehaviour {


    [SyncVar]
	public float max_health = 100f;
    [SyncVar]
	public float cur_health = 0f;
	public GameObject HealthBar;
	// Use this for initialization
	void Start () {
		cur_health = max_health;
		InvokeRepeating("decreasehealth", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    [Command]
    void CmdDestroyNetworkIdentity(NetworkInstanceId netId) {
        GameObject obj = NetworkServer.FindLocalObject(netId);
        NetworkServer.Destroy(obj);
    }

    [Command]
    void CmdTakeDamage(NetworkInstanceId netId, int dmgToTake) {
        GameObject obj = NetworkServer.FindLocalObject(netId);
        BuilderHealth builderHealth = obj.GetComponent<BuilderHealth>();
        builderHealth.cur_health -= dmgToTake;
    }

	void decreasehealth(){
        if (!hasAuthority) {
		    setHealthbar(cur_health / max_health);
            Debug.Log("Do not have authority. cur_health is: " + cur_health);
            return;
        }

        NetworkIdentity networkIdentity = gameObject.GetComponent<NetworkIdentity>();
        CmdTakeDamage(networkIdentity.netId, 20);
		setHealthbar(cur_health / max_health);

        if (cur_health <= 0) {
            CmdDestroyNetworkIdentity(networkIdentity.netId);
        }
	}
	
	public void setHealthbar(float myHealth){
		HealthBar.transform.localScale = new Vector3 (myHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
	}
}


