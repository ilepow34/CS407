using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BuilderHealth : NetworkBehaviour {


	public GameObject HealthBar;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
            Unit unit = gameObject.GetComponent<Unit>();
		    setHealthbar(unit.TreeHealth / unit.MaxHealth);
	}

	public void setHealthbar(float myHealth){
		HealthBar.transform.localScale = new Vector3 (myHealth, HealthBar.transform.localScale.y, HealthBar.transform.localScale.z);
	}
}


