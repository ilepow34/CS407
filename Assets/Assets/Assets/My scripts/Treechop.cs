using UnityEngine;
using System.Collections;

public class Treechop : MonoBehaviour 
{

	public GameObject TreePrefab;
	public Transform Spawner;
	public float logTimer;
	public GameObject TreeArea;

	// Use this for initialization
	void OnTriggerEnter (Collider other) 
	{
		logTimer = 15.0f;
	}

// Update is called once per frame
	public void OnTriggerStay (Collider other) 
	{
		logTimer -= Time.deltaTime;
		if(logTimer <= 0.0f)
		{
			logTimer += 15.0f;
			//force parameters
			float xforce = Random.Range(-50.0f, 50.0f);
			float zforce = Random.Range(-50.0f, 50.0f);


			//									//spawn object
			GameObject gameObject = (GameObject)Instantiate(TreePrefab, Spawner.position, Spawner.rotation);
			//adding force
			gameObject.GetComponent<Rigidbody>().AddForce(xforce, 0.0f, zforce);
		}
	}	
}

/*	void OnTriggerExit (Collider other) 
	{
		Debug.Log ("Unit Exited TreeArea");	
	}
}
*/
