using UnityEngine;
using System.Collections;

public class textlayer : MonoBehaviour {

	// Use this for initialization
	[ExecuteInEditMode]
	void Start () {
		GetComponent<MeshRenderer>().sortingLayerName = "GUI";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
