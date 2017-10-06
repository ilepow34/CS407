using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	RaycastHit hit;
	public Vector3 RightClickPoint;
	public static ArrayList CurrentlySelectedUnits = new ArrayList();
	public GameObject Target;
	public static Vector3 mouseDownPoint;
	public GameObject mousedot;

		
		void Awake()
	{
		mouseDownPoint = Vector3.zero;
	}
		
	
		void Update ()
		{		
		//ray casting to find out where the ground is and what is moveable	
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
			// store point at mouse button down
			if(Input.GetMouseButtonDown(0)) mouseDownPoint = hit.point;

			if(hit.transform.tag == "Ground")
			{
				if(Input.GetMouseButtonDown(1))
				{
					Target.transform.position = hit.point;

					for(int i = 0; i < CurrentlySelectedUnits.Count; i++)
					{
						Debug.Log ("move");
						Unit unit = ((GameObject)CurrentlySelectedUnits[i]).GetComponent<Unit>();
						unit.NavMeshMoveUnit(Target.transform);
					}

				}

			}// end of Ground

			else if(hit.transform.tag == "Unit")
			{
				// hitting other objects
				if (Input.GetMouseButtonUp(0) && DidUserClickLeftMouse(mouseDownPoint))
				{
					// is the user hitting the unit? ie. something selectable
					if (hit.collider.transform.Find("Selected"))
					{
						// found a selectable unit
						Debug.Log(CurrentlySelectedUnits.Count);
						
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
	
	#endregion
}