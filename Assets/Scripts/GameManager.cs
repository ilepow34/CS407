using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
	{


	public GameObject[] buildings;

	public GameObject[] units;


	public GameObject[] resources;

	public int money = 0;

	public UnitEnum unitToSpawn = UnitEnum.Building;
	public int unitCost = 0;



	public GameObject[] addToArray (GameObject addition, GameObject[] array){
		var tempArray = new GameObject[array.Length + 1];
		for (int i = 0; i< array.Length; i++){
			tempArray [i] = array [i];
		}
		tempArray [tempArray.Length - 1] = addition;
		return tempArray;
	}

	public void addBuildings(GameObject source){
		buildings = addToArray(source, buildings);
	}
	public void addUnits(GameObject source){
		units = addToArray(source, units);
	}
	public void addResources(GameObject source){
		resources = addToArray(source, resources);
	}

}

