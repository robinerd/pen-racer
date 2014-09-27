using UnityEngine;
using System.Collections;

public class Again : MonoBehaviour {
	
	public Color hoverColor = Color.white;
	
	Color startColor;
	
	// Use this for initialization
	void Start () {
		startColor = GetComponent<SpriteRenderer>().color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {
		Application.LoadLevel(Application.loadedLevel);
	}
	
	void OnMouseEnter() {
		GetComponent<SpriteRenderer>().color = hoverColor;
	}
	
	void OnMouseExit() {
		GetComponent<SpriteRenderer>().color = startColor;
	}
}
