
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Unit : NetworkBehaviour
{


    [SyncVar]
    public bool faction = false;
	[SyncVar]
    public string type;
    public int dmg;
    Vector3[] path;
    public int range;
    int targetIndex;
    public int fact = 0;
	private Unit triggeredUnit;


    GameObject unitlist;
    //public static bool plyrfaction = false;
    private FactionList fl;



    //public GameObject TreePrefab;

    public float logTimer;
    //public GameObject TreeArea;
    [SyncVar]
    public float TreeHealth = 100.0f;
    public float MaxHealth = 100.0f;
    private int damage = 0;
    public Transform Spawner;
	public float cost;

    public float spawnTime;
	
    private void Update()
    {



    }

	[Command]
    void CmdDestroyNetworkIdentity(NetworkInstanceId netId) {
		if (!isServer) {
			return;
		}
        GameObject obj = NetworkServer.FindLocalObject(netId);
        fl = unitlist.GetComponent<FactionList>();
        fl.removeUnit(this);
        NetworkServer.Destroy(obj);
    }

    [Command]
    void CmdTakeDamage(NetworkInstanceId netId, int dmgToTake) {
        GameObject obj = NetworkServer.FindLocalObject(netId);
        if (obj != null)
        {
            Unit unit = obj.GetComponent<Unit>();
            unit.TreeHealth -= dmgToTake;
            if (unit.TreeHealth <= 0)
            {
                // probably fucking doesnt
                //	CmdDestroyNetworkIdentity(netId);
                Debug.Log("removing RIP");
                fl = unitlist.GetComponent<FactionList>();
                fl.removeUnit(this);
                // actually works
                NetworkServer.Destroy(obj);
            }
        }
    }

    void decreaseHealth() {
        if (!hasAuthority) {
            return;
        }
        NetworkIdentity networkIdentity = gameObject.GetComponent<NetworkIdentity>();
        CmdTakeDamage(networkIdentity.netId, 20);
        if (TreeHealth <= 0) {
            //DestroyNetworkIdentity(networkIdentity.netId);
        }
    }

    void Start()
    {
	spawnTime = Time.timeSinceLevelLoad;
        // for debugging

        //TextMesh tt = gameObject.AddComponent(TextMesh);
        //faction = GameControl.plyrfaction;

        //wallTxt.transform.LookAt(Camera.main.transform.position, Vector3.right);

  
        if (faction)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            //gameObject.GetComponent<Renderer>. = Color.red;

        }
        MaxHealth = TreeHealth;
        unitlist = GameObject.Find("mgrGame");
    }
    //applying damage to tree
    public void ApplyDamage(float damage)
    {
        TreeHealth -= damage;
        if (TreeHealth <= 0.0) { Destroy(this.gameObject); }

    }
    public bool getFaction()
    {
        return faction;
    }
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
		if(!hasAuthority) {
			return;
		}
		
        if (other.tag == "Unit")
        {

            Unit collunit = other.gameObject.GetComponentInParent<Unit>();

	    if (type.Equals("barracks") && type.Equals(collunit.type)) {
		GameObject toDelete = (spawnTime < collunit.spawnTime) ? collunit.gameObject : gameObject;

        	NetworkIdentity networkIdentity = toDelete.GetComponent<NetworkIdentity>();
		CmdDestroyNetworkIdentity(toDelete.GetComponent<NetworkIdentity>().netId);

		// TODO: give the player that lost the barracks their money back?

 	        Debug.Log("delete one of the units.");
		return;
            }
		    
	    // bool temp = collunit.faction;
            if (collunit.faction != faction)
            {
				if(triggeredUnit == null){
					triggeredUnit = collunit;
				}
                damage = collunit.dmg;
                Debug.Log("heyhey");
            }

        }
        else damage = 0;
        logTimer = 5.0f;
        if (type.Equals("barracks"))
            logTimer = 15.0f;
    }
	
	void OnTriggerExit(Collider other){
				if(!hasAuthority) {
			return;
		}
		
		if(other.tag == "Unit"){
			Unit collunit = other.gameObject.GetComponentInParent<Unit>();
			if(collunit == triggeredUnit){
				triggeredUnit = null;
			}
		}
	}

    // Update is called once per frame
    public void OnTriggerStay(Collider other)
    {
				if(!hasAuthority) {
			return;
		}
		
       // Debug.Log(other.tag + "is other tag???");
        if (other.tag == "Unit")
        {
            Unit collunit = other.gameObject.GetComponentInParent<Unit>();

           // Debug.Log("Enemy name is" + collunit.name + " Enemy Faction is " + collunit.fact);
           // Debug.Log("I am " + gameObject.name + " My Faction is " + fact);
            if (collunit.faction != faction)
            {
				if(triggeredUnit == null){
					triggeredUnit = collunit;
				}
				if(collunit == triggeredUnit){
					logTimer -= Time.deltaTime;
					if (logTimer <= 0.0f)
					{

						Debug.Log("HURTS");

					//  ApplyDamage(damage);
						CmdTakeDamage(collunit.GetComponent<NetworkIdentity>().netId, dmg);

						//getdmg(damage);
						logTimer += 5.0f;
					}
				}
                else{
					Debug.Log("Not triggered Unit");
				}
            }

            else if (collunit.faction == faction && type.Equals("barracks"))
            {

                logTimer -= Time.deltaTime;
                if (logTimer <= 0.0f)
                {
                    logTimer += 15.0f;

                    float xforce = Random.Range(-50.0f, 50.0f);
                    float zforce = Random.Range(-50.0f, 50.0f);
                    if (other.tag == "Unit")
                    {

                        GameManager gameManager = Toolbox.RegisterComponent<GameManager>();
                        if (gameManager.money >= 20)
                        {
                            gameManager.money -= 20;
							if (other.transform != null && other.transform.parent != null && other.transform.parent.gameObject != null && Spawner != null){
                           		GameObject gameObject = (GameObject)Instantiate(other.transform.parent.gameObject, Spawner.position, Spawner.rotation);
                            	gameObject.GetComponent<Rigidbody>().AddForce(xforce, 0.0f, zforce);
							}
                        }
                        else
                        {
                            Debug.Log("Not enough money");
                        }
                    }
                }
            }
        }
    }

    public void NavMeshMoveUnit(Transform target)
    {
		UnityEngine.AI.NavMeshAgent navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		if (navMeshAgent != null)
			navMeshAgent.SetDestination(target.position);
    }

    public void attack(Transform target)
    {
        float distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
        UnityEngine.AI.NavMeshAgent navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (distance >= range)
            navMeshAgent.SetDestination(target.position);
        else navMeshAgent.enabled = false;



    }

}
