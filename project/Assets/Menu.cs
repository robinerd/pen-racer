using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	public GameObject popup;
	
	bool visible = false;
	
	void OnMouseDown() {
		visible = !visible;
		popup.SetActive(visible);
	}
}
