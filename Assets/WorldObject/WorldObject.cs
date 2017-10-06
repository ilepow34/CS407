using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour {

    public string objectName;
    public Texture2D buildImage;
    public int cost, sellValue, hitPOints, maxHitPoints;


    protected PlayerPrefs player;
    protected string[] actions = { };
    protected bool currentlySelected = false;
	// Use this for initialization

    protected virtual void Awake()
    {

    }
	protected virtual void Start () {
     //   player = transform.root.GetComponentInChildren<Player>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		
	}
    protected virtual void OnGui()
    {

    }
}
