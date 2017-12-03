
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

    public TextMesh textMeshPrefab;


    

    //public GameObject TreePrefab;

    public float logTimer;
    //public GameObject TreeArea;
    [SyncVar]
    public float TreeHealth = 100.0f;
    public float MaxHealth = 100.0f;
    private int damage = 0;
    public Transform Spawner;
    TextMesh wallTxt;
    private void Update()
    {
        wallTxt.transform.LookAt(Camera.main.transform.position);
        wallTxt.transform.RotateAround(wallTxt.transform.position, wallTxt.transform.up, 180f);


    }

    [Command]
    void CmdDestroyNetworkIdentity(NetworkInstanceId netId) {
        GameObject obj = NetworkServer.FindLocalObject(netId);
        NetworkServer.Destroy(obj);
    }

    [Command]
    void CmdTakeDamage(NetworkInstanceId netId, int dmgToTake) {
        GameObject obj = NetworkServer.FindLocalObject(netId);
        Unit unit = obj.GetComponent<Unit>();
        unit.TreeHealth -= dmgToTake;
    }

    void decreaseHealth() {
        if (!hasAuthority) {
            return;
        }
        NetworkIdentity networkIdentity = gameObject.GetComponent<NetworkIdentity>();
        CmdTakeDamage(networkIdentity.netId, 20);
        if (TreeHealth <= 0) {
            CmdDestroyNetworkIdentity(networkIdentity.netId);
        }
    }

    void Start()
    {
        // for debugging
        InvokeRepeating("decreaseHealth", 1.0f, 1.0f);

        //TextMesh tt = gameObject.AddComponent(TextMesh);
        //faction = GameControl.plyrfaction;
        wallTxt = Instantiate(textMeshPrefab,Vector3.zero, Quaternion.identity);
        wallTxt.transform.position = gameObject.transform.position;
        wallTxt.transform.rotation = gameObject.transform.rotation;
        wallTxt.transform.SetParent(gameObject.transform);
        //wallTxt.transform.LookAt(Camera.main.transform.position, Vector3.right);
        range = 15;
        dmg = 10;
        if (type.Equals("builder"))
        {
            TreeHealth = 50;
            range = 10;
            dmg = 5;
        }
        else if (type.Equals("barracks"))
        {
            dmg = 0;
            TreeHealth = 300;
            range = 10;
        }
        else if (type.Equals("turret"))
        {
            TreeHealth = 250;
            range = 10;
            dmg = 20;
        }
        else if (type.Equals("soldier"))
        {
            TreeHealth = 100;
            dmg = 10;
        }
        else if (type.Equals("heavy"))
        {
            TreeHealth = 150;
            dmg = 15;
        }
        else if (type.Equals("tank"))
        {
            TreeHealth = 500;
            dmg = 20;
        }
        else if (type.Equals("commando"))
        {
            TreeHealth = 250;
            range = 20;
            dmg = 20;
        }
        if (faction)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            //gameObject.GetComponent<Renderer>. = Color.red;

        }
        MaxHealth = TreeHealth;
        wallTxt.text = "" + TreeHealth;
    }
    //applying damage to tree
    public void ApplyDamage(float damage)
    {
        TreeHealth -= damage;
        wallTxt.text = "" + TreeHealth;
        if (TreeHealth <= 0.0) { Destroy(this.gameObject); }

    }
    public bool getFaction()
    {
        return faction;
    }
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Unit")
        {
            Unit collunit = other.gameObject.GetComponentInParent<Unit>();
            // bool temp = collunit.faction;
            if (collunit.faction != faction)
            {
                damage = collunit.dmg;
                Debug.Log("heyhey");
            }

        }
        else damage = 0;
        logTimer = 5.0f;
        if (type.Equals("barracks"))
            logTimer = 15.0f;
    }

    // Update is called once per frame
    public void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Unit")
        {
            Unit collunit = other.gameObject.GetComponentInParent<Unit>();

            Debug.Log("Enemy name is" + collunit.name + " Enemy Faction is " + collunit.fact);
            Debug.Log("I am " + gameObject.name + " My Faction is " + fact);
            if (collunit.faction != faction)
            {
                logTimer -= Time.deltaTime;
                if (logTimer <= 0.0f)
                {

                    Debug.Log("HURTS");

                    ApplyDamage(damage);
                    //getdmg(damage);
                    logTimer += 5.0f;

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

                            GameObject gameObject = (GameObject)Instantiate(other.transform.parent.gameObject, Spawner.position, Spawner.rotation);
                            gameObject.GetComponent<Rigidbody>().AddForce(xforce, 0.0f, zforce);
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
