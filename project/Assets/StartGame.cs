using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {
	
	public int numPlayers = -1;
	
	// Use this for initialization
	void Start () {
		if(numPlayers == -1) {
			Debug.LogError("Error: invalid player count in StartGame");
		}
	}
	
	void OnMouseDown() {
		Player.numPlayers = numPlayers;
		Application.LoadLevel(Application.loadedLevel);
		Player.initStatic();
	}
}
