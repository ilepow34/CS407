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
	public GameObject unitToSpawn;
	
    public static bool plyrfaction = false;
    
	private bool runOnce = false;
	
    // position, team, etc things the server needs to know
	[Command]
	void CmdSpawnInitBuilder(Vector3 position, Quaternion rotation, int connectionId, bool fact)
	{

        // instantiate object on server
		GameObject builder = Instantiate(builderPrefab, position,rotation) as GameObject;
		
        // manipulate anything. this was just for testing to see if it syncd properties
        builder.GetComponent<Unit>().faction = fact;
		builder.GetComponent<Unit>().type = "SPAWNED FROM SERVER";
		Debug.Log("Spawninbg shit: " + fact);

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

		// this then spawns it on clients and sets the owner properly
		NetworkServer.SpawnWithClientAuthority(building, NetworkServer.connections[connectionId]);
	}

	//Changes the unit to be placed based
	//Buttons in the GUI will cause different values to be passed to this
	public void ChangeUnit(int n) {
		GameManager gameManager = Toolbox.RegisterComponent<GameManager>();
		gameManager.unitToSpawn = (UnitEnum) n;
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
				CmdSpawnInitBuilder(new Vector3( plyrfaction ? 0.0f : 0.0f, 0.0f, 0.0f),
							Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId, plyrfaction);
				
	//SceneManager.LoadScene("Game");
}
	
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
			Debug.Log ((UnitEnum) 0);

		}

		//ray casting to find out where the ground is and what is moveable	
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
			// store point at mouse button down
			if(Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(2)|| Input.GetMouseButtonDown(1)) mouseDownPoint = hit.point;

            if (hit.transform.tag == "Ground")
            {
				//Unit/building spawning
                if (Input.GetMouseButtonDown(2))
                {
                    Debug.Log("build is pressed");
                    GameManager gameManager = Toolbox.RegisterComponent<GameManager>();
					if (gameManager.money >= gameManager.unitCost)
                    {
						if (gameManager.unitToSpawn == UnitEnum.Building) {
							CmdSpawnBuilding(mouseDownPoint, Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId, plyrfaction);
						}
						if (gameManager.unitToSpawn == UnitEnum.Builder) {
							CmdSpawnInitBuilder(mouseDownPoint, Quaternion.identity, Toolbox.RegisterComponent<NetworkData>().client.connection.connectionId, plyrfaction);
						}
						gameManager.money -= gameManager.unitCost;
                    }
                    else
                    {
                        Debug.Log("Not enough money");
                    }
                }


                if (Input.GetMouseButtonDown(1))
                {
                    Target.transform.position = hit.point;

                    for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
                    {
                        Debug.Log("move");
                        Unit unit = ((GameObject)CurrentlySelectedUnits[i]).GetComponent<Unit>();
                        unit.NavMeshMoveUnit(Target.transform);
                    }

                }
                if (Input.GetMouseButtonDown(0)) DeselectGameObjectsIfSelected();
            }// end of Ground
             /*else if(hit.transform.tag == "Bldg"){
                 if (Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseDownPoint))
                 {
                     // is the user hitting the unit? ie. something selectable
                     if (hit.collider.transform.Find("Selected"))
                     {

                         // found a selectable unit
                         Debug.Log("HIT: "+CurrentlySelectedUnits.Count);

                         // if the shiftkey is NOT down, remove all the units
                         if (!ShiftKeysDown ()) {
                             Debug.Log ("shift isnt down");
                             DeselectGameObjectsIfSelected();
                         }
                         if (CurrentlySelectedUnits.Count >= 0)
                         {
                             CurrentlySelectedUnits.Add(hit.transform.gameObject);
                             GameObject selectedObj = hit.collider.transform.Find("Selected").gameObject;
                             selectedObj.SetActive(true);
                         }


                         // are we selecting a different object?
                         else if (UnitAlreadyInCurrentlySelectedUnits(hit.collider.gameObject))
                         {
                             GameObject selectedObj = hit.collider.transform.Find("Selected").gameObject;
                             selectedObj.SetActive(true);

                             // add unit to the currently selected units
                             CurrentlySelectedUnits.Add(hit.collider.gameObject);
                         }
                         else // what if the unit is already in the currently selected units
                         {
                             // we need to remove the unit
                             RemoveUnitFromCurrentlySelectedUnits(hit.collider.gameObject);
                         }
                     }
                     else // if this object is not selectable
                     {
                         if (!ShiftKeysDown())
                             DeselectGameObjectsIfSelected(); 
                     }
                 }

             }*/
            else if (hit.transform.tag == "Unit" || hit.transform.tag == "Bldg")
            {
                if (hit.transform.tag == "Bldg")
                    Debug.Log("AAHHHH");
                // hitting other objects
                if (Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseDownPoint))
                {
                    // is the user hitting the unit? ie. something selectable
                    if (hit.collider.transform.Find("Selected"))
                    {
                        // found a selectable unit
                        Debug.Log(CurrentlySelectedUnits.Count);
                        Unit collunit = hit.collider.GetComponentInParent<Unit>();
                        if (collunit.getFaction() == plyrfaction)
                        {
                            // if the shiftkey is NOT down, remove all the units
                            if (!ShiftKeysDown())
                            {
                                Debug.Log("shift isnt down");
                                DeselectGameObjectsIfSelected();
                            }
                            if (CurrentlySelectedUnits.Count >= 0)
                            {
                                CurrentlySelectedUnits.Add(hit.transform.gameObject);
                                GameObject selectedObj = hit.collider.transform.Find("Selected").gameObject;
                                selectedObj.SetActive(true);
                            }


                            // are we selecting a different object?
                            else if (UnitAlreadyInCurrentlySelectedUnits(hit.collider.gameObject))
                            {
                                GameObject selectedObj = hit.collider.transform.Find("Selected").gameObject;
                                selectedObj.SetActive(true);

                                // add unit to the currently selected units
                                CurrentlySelectedUnits.Add(hit.collider.gameObject);
                            }
                            else // what if the unit is already in the currently selected units
                            {
                                // we need to remove the unit
                                RemoveUnitFromCurrentlySelectedUnits(hit.collider.gameObject);
                            }
                        }
                        else
                        {
                            if (CurrentlySelectedUnits.Count >= 0)
                            {

                                Target.transform.position = hit.point;

                                for (int i = 0; i < CurrentlySelectedUnits.Count; i++)
                                {
                                    Debug.Log("attaccc");
                                    Unit unit = ((GameObject)CurrentlySelectedUnits[i]).GetComponent<Unit>();
                                    unit.attack(Target.transform);
                                }


                            }
                        }
                    }
                    else // if this object is not selectable
                    {
                        if (!ShiftKeysDown())
                            DeselectGameObjectsIfSelected();
                    }
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.yellow);
		}
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
	public void DeselectGameObjectsIfSelected()
	{
		if (CurrentlySelectedUnits.Count > 0)
		{
			
			for(int i = 0; i < CurrentlySelectedUnits.Count; i++)
			{
				GameObject arrayListUnit = CurrentlySelectedUnits[i] as GameObject;
				arrayListUnit.transform.Find("Selected").gameObject.SetActive(false);
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
					arrayListUnit.transform.Find("Selected").gameObject.SetActive(false);
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
