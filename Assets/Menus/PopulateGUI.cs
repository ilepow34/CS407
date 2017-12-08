using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGUI : MonoBehaviour 
{
	public GameObject tile; // the prefab to spawn in scrollview


    // for debugging
    UnitEnum randomEnum()
    {
        // types I need are: Builder, Soldier, Default, Tank
        int num = Random.Range(0, 4);
        if (num == 0) {
            return UnitEnum.Builder;
        } else if (num == 1) {
            return UnitEnum.Soldier;
        } else if (num == 2) {
            return UnitEnum.Tank;
        }
        return UnitEnum.Default;
    }
	
	void Start()
	{
		// debug code to test filling in the grid with stuff
		
		GameObject newObj;
		int numToCreate = 30;
		for (int i = 0; i < numToCreate; i++) {
			newObj = (GameObject)Instantiate(tile, transform);
            UnitThumnail thumnail = newObj.GetComponent<UnitThumnail>();
            thumnail.type = randomEnum();
			//newObj.transform.SetParent(transform, false);
		}

	}

	void Update()
	{
		// get the selected units here and populate the grid from it
	}
}
