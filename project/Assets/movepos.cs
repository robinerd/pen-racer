using UnityEngine;
using System.Collections;

public class movepos : MonoBehaviour {
	
	public Player player;
	
	// Use this for initialization
	void Awake () {
		player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
		player.moveTo(transform.position);
	}
	
	public void OnMove() {
		//disable positions in the same position as the player.
		if(Vector2.Distance(transform.position, player.transform.position) < 0.1f) {
			gameObject.SetActive(false);
		}
		else {
			gameObject.SetActive(true);
		}
	}
}
