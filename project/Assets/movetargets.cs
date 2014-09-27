using UnityEngine;
using System.Collections;

public class movetargets : MonoBehaviour {
	
	public Transform moveposPrefab;
	
	movepos[] posChildren;
	
	// Use this for initialization
	void Start () {
		for(int offsetX = -1; offsetX <= 1; offsetX++) {
			for(int offsetY = -1; offsetY <= 1; offsetY++) {
				Transform movepos = Instantiate(
					moveposPrefab, 
					(Vector2) transform.position + Level.gridSize * new Vector2(offsetX, offsetY),
					Quaternion.identity) as Transform;
				movepos.parent = transform;
			}
		}
		posChildren = GetComponentsInChildren<movepos>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnMove() {
		foreach(movepos pos in posChildren) {
			pos.OnMove();
		}
	}
}
