using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitThumnail : MonoBehaviour {
    public Sprite soldierSprite, builderSprite, tankSprite, defaultSprite;
    public UnitEnum type = UnitEnum.Default;
    private Image imageComponent = null;

	// Use this for initialization
	void Start () {
	    imageComponent = GetComponent<Image>();	
	}
	
	// Update is called once per frame
	void Update () {
        if (imageComponent == null) {
            return;
        }
	    switch (type) {
            case UnitEnum.Builder:
                imageComponent.sprite = builderSprite;
                break;
            case UnitEnum.Tank:
                imageComponent.sprite = tankSprite;
                break;
            case UnitEnum.Soldier:
                imageComponent.sprite = soldierSprite;
                break;
            case UnitEnum.Default:
                imageComponent.sprite = defaultSprite;
                break;
            default:
                break;
        }
	}
}
