using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UnitSelection : MonoBehaviour{
	public RectTransform selectBox;
	private GameObject sbGO;

    bool isSelecting = false;
    Vector3 mousePosition1;

	public static List<SelectableUnit> selectedObjects = new List<SelectableUnit>();
	public static List<SelectableUnit> suc = new List<SelectableUnit>();//added while unit is spawned and removed when destroyed

	Camera cam;//main camera reference
	Vector2 v2 = new Vector2();
	Vector2 v2f = new Vector2();

	void Start(){
		cam = Camera.main;
		sbGO = selectBox.gameObject;
		sbGO.SetActive(false);
	}

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            isSelecting = true;
            mousePosition1 = Input.mousePosition;

			selectedObjects.Clear();
			for(int i = 0; i < suc.Count; i++){
				suc[i].selectionMark.enabled = false;
			}
			sbGO.SetActive(true);
        }
        if(Input.GetMouseButtonUp(0)){
			selectedObjects.Clear();
			for(int i = 0; i < suc.Count; i++){
				if(IsWithinSelectionBounds(suc[i].gameObject)){
					selectedObjects.Add(suc[i]);
				}
			}

			sbGO.SetActive(false);
            isSelecting = false;
        }

        if(isSelecting){
			for(int i = 0; i < suc.Count; i++){
				if(IsWithinSelectionBounds(suc[i].gameObject)){
					suc[i].selectionMark.enabled = true;
				}
				else{
					suc[i].selectionMark.enabled = false;
				}
			}

			DrawSelectBox();
        }
    }

	public void DrawSelectBox(){
		Vector3 screenPosition1 = mousePosition1, screenPosition2 = Input.mousePosition;

		screenPosition1.x -= Screen.width / 2;
		screenPosition1.y -= Screen.height / 2;

		v2.Set(screenPosition2.x - mousePosition1.x, mousePosition1.y - screenPosition2.y);
		v2f.x = Mathf.Abs(v2.x);
		v2f.y = Mathf.Abs(v2.y); 
		selectBox.sizeDelta = v2f;

		if (v2.x < 0)
			screenPosition1.x -= selectBox.sizeDelta.x / 2;
		else
			screenPosition1.x += selectBox.sizeDelta.x / 2;
		if (v2.y < 0)
			screenPosition1.y += selectBox.sizeDelta.y / 2;
		else
			screenPosition1.y -= selectBox.sizeDelta.y / 2;

		selectBox.localPosition = screenPosition1;
	}

    public bool IsWithinSelectionBounds(GameObject gameObject){
        if(!isSelecting)
            return false;

		Bounds viewportBounds = Utils.GetViewportBounds(cam, mousePosition1, Input.mousePosition);

        return viewportBounds.Contains(cam.WorldToViewportPoint(gameObject.transform.position));
    }

	//generate color only once instead of new one all the time
//	private Color colD1 = new Color(0.8f, 0.8f, 0.95f, 0.25f);
//	private Color colD2 = new Color(0.8f, 0.8f, 0.95f);
//
//    void OnGUI(){
//        if(isSelecting){
//            // Create a rect from both mouse positions
//
//			//Return values can get expensive, and since function isnt used anywhere else, it is best to put it all here
////          	Rect rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
//			Vector3 screenPosition1 = mousePosition1, screenPosition2 = Input.mousePosition;
//
//			screenPosition1.y = Screen.height - screenPosition1.y;
//			screenPosition2.y = Screen.height - screenPosition2.y;
//			// Calculate corners
//			Vector3 topLeft = Vector3.Min(screenPosition1, screenPosition2);
//			Vector3 bottomRight = Vector3.Max(screenPosition1, screenPosition2);
//			// Create Rect
//			Rect rect = Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
//
//			Utils.DrawScreenRect(rect, colD1);
//			Utils.DrawScreenRectBorder(rect, 2, colD2);
//        }
//    }
}