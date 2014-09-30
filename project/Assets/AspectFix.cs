using UnityEngine;
using System.Collections;

public class AspectFix : MonoBehaviour {
	
	float orthosize;
	
	// Use this for initialization
	void Start () {
		orthosize = camera.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		int targetWidth = Screen.height * 16 / 9;
		float compensation = (float) targetWidth / Screen.width;
		camera.orthographicSize = orthosize * compensation;
	}
}
