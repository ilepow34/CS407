using UnityEngine;
using System.Collections;

public class TreechopNew : MonoBehaviour 
{
	
	public GameObject TreePrefab;
	public Transform Spawner;
	public float logTimer;
	public GameObject TreeArea;
	public float TreeHealth = 100.0f;
	public float damage = 50.0f;
	
	
	//applying damage to tree
	public void ApplyDamage (float damage)
	{
		TreeHealth =- damage;
		
		if (TreeHealth <= 0.0)
			return;
	}
	// Use this for initialization
	void OnTriggerEnter (Collider other) 
	{
		logTimer = 10.0f;
	}
	
	// Update is called once per frame
	public void OnTriggerStay (Collider other) 
	{
		logTimer -= Time.deltaTime;
		if(logTimer <= 0.0f)
		{
			logTimer += 10.0f;
			//force parameters
			float xforce = Random.Range(-50.0f, 50.0f);
			float zforce = Random.Range(-50.0f, 50.0f);

            GameManager gameManager = Toolbox.RegisterComponent<GameManager>();
        
                gameManager.money += 20;
                // Vector3 rayInfo;
                // Instantiate(bldg, mouseDownPoint, Quaternion.identity);
                //GameObject gameObject = (GameObject)Instantiate(other.transform.parent.gameObject, Spawner.position, Spawner.rotation);
                gameObject.GetComponent<Rigidbody>().AddForce(xforce, 0.0f, zforce);
           
          
            //									//spawn object
           //GameObject gameObject = (GameObject)Instantiate(TreePrefab, Spawner.position, Spawner.rotation);
			//adding force
			//gameObject.GetComponent<Rigidbody>().AddForce(xforce, 0.0f, zforce);
		}
	}	
}

/*	void OnTriggerExit (Collider other) 
	{
		Debug.Log ("Unit Exited TreeArea");	
	}
}
*/
