using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGUI : MonoBehaviour 
{
	public GameObject tile; // the prefab to spawn in scrollview
	
	void Start()
	{
		// debug code to test filling in the grid with stuff
		
		GameObject newObj;
		int numToCreate = 30;
		for (int i = 0; i < numToCreate; i++) {
			newObj = (GameObject)Instantiate(tile, transform);
			//newObj.transform.SetParent(transform, false);
		}

	}

	void Update()
	{
		// get the selected units here and populate the grid from it
	}
}
