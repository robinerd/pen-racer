using UnityEngine;
using System.Collections;

public class Again : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
		Application.LoadLevel(Application.loadedLevel);
		Player.initStatic();
	}
	
}
