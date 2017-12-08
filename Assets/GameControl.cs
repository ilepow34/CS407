using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControl : NetworkBehaviour {

	RaycastHit hit;
	public Vector3 RightClickPoint;
	public static ArrayList CurrentlySelectedUnits = new ArrayList();
	public GameObject Target;
	public static Vector3 mouseDownPoint;
	public GameObject mousedot;
	public GameObject bldg;
	public GameObject builderPrefab;
	public GameObject soldierPrefab;
	public GameObject tankPrefab;
	public GameObject defenseTowerPrefab;
	public GameObject unitToSpawn;
    public GameObject[] gos;
	int[] costs = { 10, 20, 50, 30, 50 }; //good god this is a horrible hack
   // public GameObject[] spsB;
  //  public GameObject[] spsR;
    public static bool plyrfaction = false;
    
	private bool runOnce = false;

	bool isSelecting = false;
	Vector3 mousePosition1;

    GameObject unitlist;
    //public static bool plyrfaction = false;
    private FactionList fl;


    // position, team, etc things the server needs to know
	/*
    [Command]
	void CmdSpawnInitBuilder(Vector3 position, Quaternion rotation, int connectionId, bool fact)
	{

        // instantiate object on server
		GameObject builder = Instantiate(builderPrefab, position,rotation) as GameObject;
		
        // manipulate anything. this was just for testing to see if it syncd properties
        builder.GetComponent<Unit>().faction = fact;
		builder.GetComponent<Unit>().type = "SPAWNED FROM SERVER";
		Debug.Log("Spawninbg shit: " + fact);
        unitlist = GameObject.Find("mgrGame");
        fl = unitlist.GetComponent<FactionList>();
        fl.addUnit(builder.GetComponent<Unit>());
        // this then spawns it on clients and sets the owner properly
        NetworkServer.SpawnWithClientAuthority(builder, NetworkServer.connections[connectionId]);
	}

	//Straight copied this from Nick's function
	[Command]
	void CmdSpawnBuilding(Vector3 position, Quaternion rotation, int connectionId, bool fact)
	{
        Debug.Log("Running cmd spawn");

		// instantiate object on server
		GameObject building = Instantiate(bldg, position,rotation) as GameObject;

		// manipulate anything. this was just for testing to see if it syncd properties
		building.GetComponent<Unit>().faction = fact;
		building.GetComponent<Unit>().type = "SPAWNED FROM SERVER2";
		Debug.Log("Spawninbg2900090909090909090 shit: " + fact);
        unitlist = GameObject.Find("mgrGame");
        fl = unitlist.GetComponent<FactionList>();
        fl.addUnit(building.GetComponent<Unit>());
        // this then spawns it on clients and sets the owner properly
        NetworkServer.SpawnWithClientAuthority(building, NetworkServer.connections[connectionId]);
	}
	*/

	//Refactored this shit so we don't have multiple copies
	// position, team, etc things the server needs to know
	[Command]
	void CmdSpawnUnit(UnitEnum uEnum, Vector3 position, Quaternion rotation, int connectionId, bool fact)
	{
		Debug.Log("Running cmd spawn");
		GameObject go = null;
		
		//instantiate object on server
		if (uEnum == UnitEnum.Builder) {
			go = Instantiate(builderPrefab, position,rotation) as GameObject;
		}
		if (uEnum == UnitEnum.Building) {
			go = Instantiate(bldg, position,rotation) as GameObject;
		}
		if (uEnum == UnitEnum.Tank) {
			go = Instantiate(tankPrefab, position,rotation) as GameObject;
		}
		if (uEnum == UnitEnum.Soldier) {
			go = Instantiate(soldierPrefab, position,rotation) as GameObject;
		}
		if (uEnum == UnitEnum.DefenseTower) {
			go = Instantiate(defenseTowerPrefab, position,rotation) as GameObject;
		}

		if (go == null) {
			Debug.Log ("Something broke in cmd spawn");
			return;
		}

		// manipulate anything. this was just for testing to see if it syncd properties
		go.GetComponent<Unit>().faction = fact;
		unitlist = GameObject.Find("mgrGame");
		fl = unitlist.GetComponent<FactionList>();
		// this then spawns it on clients and sets the owner properly
		NetworkServer.SpawnWithClientAuthority(go, NetworkServer.connections[connectionId]);
		fl.AddUnit(go.GetComponent<Unit>().GetComponentInParent<NetworkIdentity>().netId);
	}
    [Command]
    void CmdSpawnUnitGo(GameObject gg, Vector3 position, Quaternion rotation, int connectionId, bool fact)
    {
        Debug.Log("Running cmd spawn");
        GameObject go = null;

        //instantiate object on server

            go = Instantiate(gg, position, rotation) as GameObject;
    

        if (go == null)
        {
            Debug.Log("Something broke in cmd spawn");
            return;
        }

        // manipulate anything. this was just for testing to see if it syncd properties
        go.GetComponent<Unit>().faction = fact;
        go.GetComponent<Unit>().type = "SPAWNED FROM SERVER2";
        Debug.Log("Spawninbg2900090909090909090 shit: " + fact);
        unitlist = GameObject.Find("mgrGame");
        fl = unitlist.GetComponent<FactionList>();
        // this then spawns it on clients and sets the owner properly
        NetworkServer.SpawnWithClientAuthority(go, NetworkServer.connections[connectionId]);
        fl.AddUnit(go.GetComponent<Unit>().GetComponentInParent<NetworkIdentity>().netId);
    }

    //Changes the unit to be placed based
    //Buttons in the GUI will cause different values to be passed to this
	public void ChangeUnit(int unit) {
		GameManager gameManager = Toolbox.RegisterComponent<GameManager>();
		gameManager.unitToSpawn = (UnitEnum) unit;
		gameManager.unitCost = costs[unit];
	}

    //returns player faction
    public static bool getFaction()
    {
        return plyrfaction;
    }

    // this is just a sleep timer essentially to allow both clients to load the game before begging to spawn units.
    //
    // runs on client.
    // calls a command that then runs on server
    // ALL commands run on server always
    // so we pass any information the server needs through the command parameters
    IEnumerator WaitForIt(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);


        Debug.Log("Calling shit?");
        CmdSpawnUnit(UnitEnum.Builder, new Vector3(plyrfaction ? 0.0f : 0.0f, 0.0f, 0.0f),
                            Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId, plyrfaction);
        CmdSpawnUnit(UnitEnum.Building, new Vector3(-50.0f , 0.0f, -100.0f),
                           Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId, plyrfaction);
        /*for (int i = 0; i < gos.Length; i++)
        {
            Vector3 vct = plyrfaction ? new Vector3(-50.0f * i, 0.0f, 100.0f) : new Vector3(-50.0f * i, 0.0f, -100.0f);
            CmdSpawnUnitGo(gos[i], vct,
                           Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId, plyrfaction);


        }*/
        CmdSpawnUnit(UnitEnum.Building, new Vector3(plyrfaction ? -50.0f : 0.0f, 0.0f, 100.0f),
                           Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId, !plyrfaction);
    }
				
	//SceneManager.LoadScene("Game");

	
	void Awake()
	{
		mouseDownPoint = Vector3.zero;
	}
		
	
	void Update ()
		{
		if (!isLocalPlayer) {
			return;
		}		
		
		if (!runOnce) {			 
			StartCoroutine(WaitForIt (3.0F)); 
			runOnce = true;	
		}

        // assuming screen height of 600 px
        // we should ignore all hits on bottom 100

        float percentToIgnore = 135.0f / 600.0f;
        float currentPercent = Input.mousePosition[1] / Screen.height;
        if (currentPercent < percentToIgnore) {
            isSelecting = false;
            return;
        }


		//ray casting to find out where the ground is and what is moveable	
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        if (!Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            return;
        }
			
		// If we press the left mouse button, save mouse location and begin selection
		if( Input.GetMouseButtonDown( 0 ) )
		{
            // check if it hit a UI element?


			DeselectGameObjectsIfSelected ();
			isSelecting = true;
			mousePosition1 = Input.mousePosition;
		}
		// If we let go of the left mouse button, end selection
		if (Input.GetMouseButtonUp (0)) {
			isSelecting = false;
		}
		//if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
			// store point at mouse button down
			if(Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(2)|| Input.GetMouseButtonDown(1)) mouseDownPoint = hit.point;

				
            if (hit.transform.tag == "Ground" || hit.collider.tag == "UnitCreation")
            {
				//Unit/building spawning
               


                if (Input.GetMouseButtonDown(1))
                {
                    Target.transform.position = hit.point;

                    for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
                    {
                        Debug.Log("move");
						Debug.Log (CurrentlySelectedUnits.Count);
                        Unit unit = ((GameObject)CurrentlySelectedUnits[i]).GetComponent<Unit>();
                        unit.NavMeshMoveUnit(Target.transform);
                    }
                }
            }// end of Ground
             
			 if(hit.collider.tag == "UnitCreation"){
					//Debug.Log("It hit Unit Creation tag");
					if (Input.GetMouseButtonDown(2))
					{
						Debug.Log("build is pressed");
						GameManager gameManager = Toolbox.RegisterComponent<GameManager>();
						if (gameManager.money >= gameManager.unitCost)
						{
							CmdSpawnUnit(gameManager.unitToSpawn, mouseDownPoint, Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId, plyrfaction);
							/*
							if (gameManager.unitToSpawn == UnitEnum.Building) {
								CmdSpawnBuilding(mouseDownPoint, Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId, plyrfaction);
							}
							if (gameManager.unitToSpawn == UnitEnum.Builder) {
								CmdSpawnInitBuilder(mouseDownPoint, Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId, plyrfaction);
							}
							*/
							gameManager.money -= gameManager.unitCost;
						}
						else
						{
							//Debug.Log("Not enough money");
						}
					}
				}
			 
            else if (hit.transform.tag == "Unit" || hit.transform.tag == "Bldg")
            {
                if (hit.transform.tag == "Bldg")
                   // Debug.Log("AAHHHH");
                // hitting other objects
                if (Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseDownPoint))
                {
                    // is the user hitting the unit? ie. something selectable
                    if (hit.collider.transform.Find("Selected"))
                    {
                        // found a selectable unit
                      //  Debug.Log(CurrentlySelectedUnits.Count);
                        Unit collunit = hit.collider.GetComponentInParent<Unit>();
                        if (collunit.getFaction() == plyrfaction)
                        {
                            CurrentlySelectedUnits.Add(hit.transform.gameObject);
                            GameObject selectedObj = hit.collider.transform.Find("Selected").gameObject;
                            selectedObj.SetActive(true);
                        }
                        else
                        {
                            if (CurrentlySelectedUnits.Count >= 0)
                            {

                                Target.transform.position = hit.point;

                                for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
                                {
                                   // Debug.Log("attaccc");
                                    Unit unit = ((GameObject)CurrentlySelectedUnits[i]).GetComponent<Unit>();
                                    unit.attack(Target.transform);
                                }


                            }
                        }
                    }
                    else // if this object is not selectable
                    {
                        DeselectGameObjectsIfSelected();
                    }
                }
          //  }
//            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.yellow);
		}
	}

	void OnGUI()
	{
		if (isSelecting) {
			// Create a rect from both mouse positions
			var rect = RectSelection.GetScreenRect( mousePosition1, Input.mousePosition );
			RectSelection.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
			RectSelection.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
			GameObject[] units = GameObject.FindGameObjectsWithTag ("Unit");
			if (units != null) {
				foreach (GameObject go in units) {
					if (IsWithinSelectionBounds (go) && go.GetComponent<Unit>() != null && go.GetComponent<Unit> ().faction == plyrfaction) {
						if (!CurrentlySelectedUnits.Contains (go)) {
							Debug.Log ("Adding unit to list");
							CurrentlySelectedUnits.Add (go);
							if (go.transform != null) {
								if (go.transform.Find ("Selected") != null) {
									go.transform.Find ("Selected").gameObject.SetActive (true);
								}
							}
						}
	//					Debug.Log ("Unit found");
					} else if (UnitAlreadyInCurrentlySelectedUnits(go)) {
						RemoveUnitFromCurrentlySelectedUnits(go);
						// deselect?
					}
				}
			}
		}
	}

	//Checks if a unit is within the selection box
	public bool IsWithinSelectionBounds( GameObject gameObject )
	{
		if( !isSelecting )
			return false;

		var camera = Camera.main;
		var viewportBounds = RectSelection.GetViewportBounds( camera, mousePosition1, Input.mousePosition );

		return viewportBounds.Contains(
			camera.WorldToViewportPoint( gameObject.transform.position ) );
	}

	#region Helper functions
	//Did User perform mouse click?
	public bool DidUserClickLeftMouse(Vector3 hitPoint)
	{
		float clickZone = 1.3f;
		
		if(
			(mouseDownPoint.x < hitPoint.x + clickZone && mouseDownPoint.x > hitPoint.x - clickZone)&&
			(mouseDownPoint.y < hitPoint.y + clickZone && mouseDownPoint.y > hitPoint.y - clickZone)&&
			(mouseDownPoint.z < hitPoint.z + clickZone && mouseDownPoint.z > hitPoint.z - clickZone)
			) 
			return true; else return false;
	}
	// deselects game object if selected
	public static void DeselectGameObjectsIfSelected()
	{
		if (CurrentlySelectedUnits.Count > 0)
		{
			
			for(int i = 0; i < CurrentlySelectedUnits.Count; i++)
			{
				GameObject arrayListUnit = CurrentlySelectedUnits[i] as GameObject;
				if (arrayListUnit.transform != null) {
					if (arrayListUnit.transform.Find ("Selected") != null) {
						if (arrayListUnit.transform.Find ("Selected").gameObject != null)
							arrayListUnit.transform.Find ("Selected").gameObject.SetActive (false);
					}
				}
			}
			CurrentlySelectedUnits.Clear();
		}
	}
	
	
	// check if a unit is already in the selected units arraylist
	public static bool UnitAlreadyInCurrentlySelectedUnits(GameObject unit)
	{
		if (CurrentlySelectedUnits.Count > 0)
		{
			
			for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
			{
				GameObject arrayListUnit = CurrentlySelectedUnits[i] as GameObject;
				if (arrayListUnit == unit)
					return true;
			}
			
			return false;
		}
		else // if there are no units 
			return false;
	}
	
	
	// remove a unit from currently selected units arraylist
	public void RemoveUnitFromCurrentlySelectedUnits(GameObject unit)
	{
		if (CurrentlySelectedUnits.Count > 0)
		{
			
			for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
			{
				GameObject arrayListUnit = CurrentlySelectedUnits[i] as GameObject;
				if (arrayListUnit == unit)
				{
					CurrentlySelectedUnits.RemoveAt(i);
					if (arrayListUnit.transform != null) {
						if (arrayListUnit.transform.Find ("Selected") != null) {
							if (arrayListUnit.transform.Find ("Selected").gameObject != null)
								arrayListUnit.transform.Find ("Selected").gameObject.SetActive (false);
						}
					}
				}
			}
			return;
		}
		else // if there are no units 
			return;
	}
	
	
	// shift key being held down 
	public static bool ShiftKeysDown()
	{
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			return true;
		else
			return false;
	}
	public static bool CtrlDown()
	{
		if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
			return true;
		else
			return false;
	}

	#endregion
}
