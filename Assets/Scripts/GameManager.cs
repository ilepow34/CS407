using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager
	{


	public static GameObject[] buildings;

	public static GameObject[] units;


	public static GameObject[] resources;

	public static int money = 0;



	public static GameObject[] addToArray (GameObject addition, GameObject[] array){
		var tempArray = new GameObject[array.Length + 1];
		for (int i = 0; i< array.Length; i++){
			tempArray [i] = array [i];
		}
		tempArray [tempArray.Length - 1] = addition;
		return tempArray;
	}

	public static void addBuildings(GameObject source){
		buildings = addToArray(source, buildings);
	}
	public static void addUnits(GameObject source){
		units = addToArray(source, units);
	}
	public static void addResources(GameObject source){
		resources = addToArray(source, resources);
	}

}

