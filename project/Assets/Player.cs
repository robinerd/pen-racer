using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public Transform tracePrefab;
	public Transform tracemarkerPrefab;
	public Transform pretracePrefab;
	public GameObject again;
	public GameObject dead;
	public GameObject win;
	public TextMesh movesText;
	
	Vector2 lastMove;
	Transform pretrace;
	int moves = 0;
	
	void Awake() {
	}
	
	// Use this for initialization
	void Start () {
		pretrace = Instantiate(pretracePrefab) as Transform;
		moveTo((Vector2)transform.position + new Vector2(Level.gridSize, 0));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void moveTo(Vector2 newPos) {
		moves++;
		if(moves == 1)
			movesText.text = "1 Move";
		else
			movesText.text = moves + " Moves";
		
		lastMove = newPos - (Vector2) transform.position;
		
		Vector3 oldPosition = transform.position;
		
		Transform trace = Instantiate(tracePrefab) as Transform;
		trace.position = transform.position;
		
		float angle =Mathf.Atan2(lastMove.y, lastMove.x);
		angle = Mathf.Rad2Deg * (angle) - 90;
		trace.Rotate(new Vector3(0,0, angle));
		
		Vector3 scale = trace.localScale;
		scale.y = lastMove.magnitude / (160.0f / 64.0f);
		trace.localScale = scale;
		
		//MOVE
		transform.position = newPos;
		
		Instantiate(tracemarkerPrefab, transform.position, Quaternion.identity);
		
		pretrace.position = transform.position;
		pretrace.localRotation = Quaternion.Euler(0,0,angle);
		pretrace.localScale = scale;
		
		
		moveTarget(lastMove);
		
		//Check if we died
		RaycastHit2D hit = Physics2D.Raycast(oldPosition, lastMove.normalized, lastMove.magnitude);
		if(hit) {
			
			pretrace.gameObject.SetActive(false);
			
			//move out of screen to hide
			moveTarget(new Vector2(0, 10000));
			
			if(hit.collider.tag == "Deadly") {
				trace.GetComponentsInChildren<CrossOver>(true)[0].gameObject.SetActive(true);
				
				//show gameover
				dead.SetActive(true);
				again.SetActive(true);
			}
			else if(hit.collider.tag == "Goal") {
				win.SetActive(true);
				again.SetActive(true);
			}
		}
	}
			
	void moveTarget(Vector2 newLocalPos) {
		transform.GetComponentInChildren<movetargets>().transform.localPosition = newLocalPos;
		GetComponentInChildren<movetargets>().OnMove();
	}
}
