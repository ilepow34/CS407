using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGUI : MonoBehaviour 
{
	public GameObject tile; // the prefab to spawn in scrollview

	void Start()
	{
    }

	void Update()
	{

        ArrayList selectedUnits = GameControl.CurrentlySelectedUnits;
        UnitThumnail[] thumnails = GetComponentsInChildren<UnitThumnail>();

        int loopCount = selectedUnits.Count;
        for (int i = 0; i < loopCount; i++) {

            // make sure it is visible if it is in here

            Unit curUnit = ((GameObject)selectedUnits[i]).GetComponent<Unit>();
            if (thumnails.Length > i) {
                if (thumnails[i] != null && thumnails[i].unit != curUnit) {
                    thumnails[i].unit = curUnit;
                }
            } else {
                // create since we need another thumnail
                GameObject newObj = (GameObject)Instantiate(tile, transform);
                newObj.GetComponent<UnitThumnail>().unit = curUnit;
            }
        }
        
        if (thumnails.Length > loopCount) {
            // hide last thumnails
            for (int i = loopCount; i < thumnails.Length; i++) {
                Destroy(thumnails[i].gameObject);
            }
        }

    }
}
