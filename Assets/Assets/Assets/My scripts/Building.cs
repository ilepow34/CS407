using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public GameObject TreePrefab;
    public Transform Spawner;
    public float logTimer;
    public GameObject TreeArea;
    public float TreeHealth = 100.0f;
    public float damage = 50.0f;


    //applying damage to tree
    public void ApplyDamage(float damage)
    {
        TreeHealth = -damage;

        if (TreeHealth <= 0.0)
            return;
    }
    // Use this for initialization
    void OnTriggerEnter(Collider other)
    {
        logTimer = 15.0f;
    }

    // Update is called once per frame
    public void OnTriggerStay(Collider other)
    {
        logTimer -= Time.deltaTime;
        if (logTimer <= 0.0f)
        {
            logTimer += 15.0f;
            //force parameters
            float xforce = Random.Range(-50.0f, 50.0f);
            float zforce = Random.Range(-50.0f, 50.0f);
            if (other.tag == "Unit")
            {
                // GameObject who =;
                GameManager gameManager = Toolbox.RegisterComponent<GameManager>();
                if (gameManager.money >= 20)
                {
                    gameManager.money -= 20;
                    // Vector3 rayInfo;
                    // Instantiate(bldg, mouseDownPoint, Quaternion.identity);
                    GameObject gameObject = (GameObject)Instantiate(other.transform.parent.gameObject, Spawner.position, Spawner.rotation);
                    gameObject.GetComponent<Rigidbody>().AddForce(xforce, 0.0f, zforce);
                }
                else
                {
                    Debug.Log("Not enough money");
                }
                //									//spawn object
               
                //adding force
              
            }
        }
    }
}

/*	void OnTriggerExit (Collider other) 
	{
		Debug.Log ("Unit Exited TreeArea");	
	}
}
*/
