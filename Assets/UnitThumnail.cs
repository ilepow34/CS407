using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitThumnail : MonoBehaviour {
    public Sprite soldierSprite, builderSprite, tankSprite, defaultSprite;
    private Image imageComponent = null;

    public Unit unit;

	// Use this for initialization
	void Start () {
	    imageComponent = GetComponent<Image>();	
	}

    public void selectUnit()
    {

        GameControl.DeselectGameObjectsIfSelected();

        GameControl.CurrentlySelectedUnits.Add(unit.gameObject);
        unit.gameObject.transform.Find("Selected").gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (imageComponent == null) {
            return;
        }
        if (unit == null) {
            imageComponent.sprite = defaultSprite;
            return;
        }
        
        if (unit.type == "builder") {
            imageComponent.sprite = builderSprite;
        } else if (unit.type == "soldier") {
            imageComponent.sprite = soldierSprite;
        } else if (unit.type == "tank") {
            imageComponent.sprite = tankSprite;
        }
	}
}
