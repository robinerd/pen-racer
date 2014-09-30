using UnityEngine;
using System.Collections;

public class movetargets : MonoBehaviour {
	
	public Transform moveposPrefab;
	
	movepos[] posChildren;
	
	// Use this for initialization
	void Awake () {
		Player player = transform.parent.GetComponent<Player>();
		Vector3 colorVec = new Vector3(player.penColor.r, player.penColor.g, player.penColor.b);
		float whiteness = 0.6f;
		colorVec = 1.0f/(1+whiteness) * (colorVec + new Vector3(whiteness, whiteness, whiteness));
		Color color = new Color(colorVec.x, colorVec.y, colorVec.z);
		
		for(int offsetX = -1; offsetX <= 1; offsetX++) {
			for(int offsetY = -1; offsetY <= 1; offsetY++) {
				Transform movepos = Instantiate(
					moveposPrefab, 
					(Vector2) transform.position + Level.gridSize * new Vector2(offsetX, offsetY),
					Quaternion.identity) as Transform;
				Vector3 position = movepos.localPosition;
				position.z = -1;
				movepos.localPosition = position;
				
				movepos.parent = transform;
				movepos.GetComponent<movepos>().player = player;
				movepos.GetComponent<SpriteRenderer>().color = color;
			}
		}
		posChildren = GetComponentsInChildren<movepos>(true);
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
