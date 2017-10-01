using UnityEngine;

public class SelectableUnit : MonoBehaviour{
	public Projector selectionMark;

	void Start(){
		UnitSelection.suc.Add(this);
	}

	void OnDestroy(){
		UnitSelection.suc.Remove(this);
	}
}