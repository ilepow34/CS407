using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	
	Vector3[] path;
	int targetIndex;
	
	public void NavMeshMoveUnit (Transform target)
	{
		UnityEngine.AI.NavMeshAgent navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		navMeshAgent.SetDestination(target.position);
	}
}
