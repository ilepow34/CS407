using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyScript : MonoBehaviour {

	public int money;
	public float moneyTimer;
	public float timerInterval;
	public int intervalAmount;
	public Text moneyText;
	// Use this for initialization
	void Start () {
		moneyTimer = 10.0f;
		timerInterval = 10.0f;
		intervalAmount = 50;
		money = 100;
		//moneyText = 
	}
	
	// Update is called once per frame
	void Update () {
		moneyTimer -= Time.deltaTime;
		//regenerate 50 money every 10 seconds
		if (moneyTimer <= 0.0f) {
			money += intervalAmount;
			moneyTimer += timerInterval;
		}
		moneyText.text = "Money: " + money.ToString ();
	}
}
