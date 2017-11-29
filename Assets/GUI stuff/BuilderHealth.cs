using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderHealth : MonoBehaviour {


	public float max_health = 100f;
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
	void decreasehealth(){
		cur_health -= 20;
		float calc_health = cur_health / max_health;
		setHealthbar(calc_health);
		destroyOnDeath();
	}
	
	void destroyOnDeath(){
		if(cur_health <= 0){
			Destroy(gameObject);
		}
		
	}
	
	public void setHealthbar(float myHealth){
		HealthBar.transform.localScale = new Vector3 (myHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
	}
}


