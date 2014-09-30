using UnityEngine;
using System.Collections;

public class HoverColor : MonoBehaviour {
	
#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID)
	public static bool isMobile = true;
#else
	public static bool isMobile = false;
#endif
	
	public Color hoverColor = Color.white;
	
	Color startColor;
	
	// Use this for initialization
	void Start () {
		startColor = GetComponent<SpriteRenderer>().color;
	}
	
	void OnMousePress() {
		if(isMobile) {
			showHighlight();
			Invoke("showNormal", 0.3f);
		}
	}
	
	void OnMouseEnter() {
		if(!isMobile)
			showHighlight();
	}
	
	void OnMouseExit() {
		if(!isMobile)
			showNormal();
	}
	
	void showHighlight() {
		GetComponent<SpriteRenderer>().color = hoverColor;
	}
	
	void showNormal() {
		GetComponent<SpriteRenderer>().color = startColor;
	}
}
