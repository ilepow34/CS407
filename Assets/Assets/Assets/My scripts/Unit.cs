
using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{


    public bool faction = false;
    public string type;
    public int dmg;
    Vector3[] path;
    public int range;
    int targetIndex;
    public int fact = 0;


    //public GameObject TreePrefab;

    public float logTimer;
    //public GameObject TreeArea;
    public float TreeHealth = 100.0f;
    private int damage = 0;

    void Start()
    {
        range = 15;
        dmg = 10;
        if (type.Equals("builder"))
        {
            TreeHealth = 50;
            range = 10;
            dmg = 5;
        }
        if (type.Equals("turret")) {
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
    }
    //applying damage to tree
    public void ApplyDamage(float damage)
    {
        TreeHealth = -damage;

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
        logTimer = 15.0f;
    }

    // Update is called once per frame
    public void OnTriggerStay(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Unit")
        {
            Unit collunit = other.gameObject.GetComponentInParent<Unit>();

            Debug.Log("Enemy name is" + collunit.name + " Enemy Facrtion is " + collunit.fact);
            Debug.Log("I am " + gameObject.name + " My Facrtion is " + fact);
            if (collunit.faction != faction)
            {


                logTimer -= Time.deltaTime;
                if (logTimer <= 0.0f)
                {
                    Debug.Log("HURTS");

                    ApplyDamage(damage);
                    //getdmg(damage);
                    logTimer += 15.0f;
                    //force parameters
                    /*float xforce = Random.Range(-50.0f, 50.0f);
                    float zforce = Random.Range(-50.0f, 50.0f);


                    //									//spawn object
                    GameObject gameObject = (GameObject)Instantiate(TreePrefab, Spawner.position, Spawner.rotation);
                    //adding force
                    gameObject.GetComponent<Rigidbody>().AddForce(xforce, 0.0f, zforce);*/
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
