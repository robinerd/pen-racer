using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public static int numPlayers = 2;
	
	static Player[] players = new Player[numPlayers];
	static bool allDead = false;
	static bool hasWinner = false;
	
	public int playerID = -1;
	public Color penColor;
	public Vector2 markerScale = new Vector2(1,1);
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
	int nextPlayer;
	bool isDead = false;
	bool isDefaultMove = true;
	
	public static void initStatic() {
		players = new Player[numPlayers];
		allDead = false;
		hasWinner = false;
	}
	
	void Awake() {
		if(playerID == -1) {
			Debug.LogError("Internal Error: Player ID invalid");
			gameObject.SetActive(false);
			return;
		}
		
		if(playerID + 1 > numPlayers) {
			gameObject.SetActive(false);
			return;
		}
		
		players[playerID] = this;
		nextPlayer = (playerID + 1) % numPlayers;
		
		pretrace = Instantiate(pretracePrefab) as Transform;
		pretrace.GetComponent<SpriteRenderer>().color = penColor;
	}
	
	// Use this for initialization
	void Start () {
		moveTo((Vector2)transform.position + new Vector2(Level.gridSize, 0));
		isDefaultMove = false;
		if(playerID == 0)
			setTargetVisible(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void makeTurn() {
		if(hasWinner) {
			return;
		}
		
		if(!isDead) {
			setTargetVisible(true);
		}
		else if(!allDead) { //prevent infinite recursion if all are dead
			players[nextPlayer].makeTurn();
		}
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
		trace.GetComponent<SpriteRenderer>().color = penColor;
		trace.GetComponentsInChildren<CrossOver>(true)[0].GetComponent<SpriteRenderer>().color = penColor;
		trace.position = transform.position;
		
		float angle =Mathf.Atan2(lastMove.y, lastMove.x);
		angle = Mathf.Rad2Deg * (angle) - 90;
		trace.Rotate(new Vector3(0,0, angle));
		
		Vector3 scale = trace.localScale;
		scale.y = lastMove.magnitude / (160.0f / 64.0f);
		trace.localScale = scale;
		
		//MOVE
		transform.position = newPos;
		
		Transform tracemarker = Instantiate(tracemarkerPrefab, transform.position, Quaternion.identity) as Transform;
		tracemarker.GetComponent<SpriteRenderer>().color = penColor;
		Vector3 tracemarkerScale = tracemarker.localScale;
		tracemarkerScale.x *= markerScale.x;
		tracemarkerScale.y *= markerScale.y;
		tracemarker.localScale = tracemarkerScale;
		
		pretrace.position = transform.position;
		pretrace.localRotation = Quaternion.Euler(0,0,angle);
		pretrace.localScale = scale;
		
		setTargetVisible(false);
		moveTarget(lastMove);
		
		//Check if we died
		RaycastHit2D hit = Physics2D.Raycast(oldPosition, lastMove.normalized, lastMove.magnitude);
		if(hit) {
			
			pretrace.gameObject.SetActive(false);
			
			//move out of screen to hide
			setTargetVisible(false);
			
			if(hit.collider.tag == "Deadly") {
				trace.GetComponentsInChildren<CrossOver>(true)[0].gameObject.SetActive(true);
				
				isDead = true;
				
				allDead = true;
				foreach(Player player in players) {
					if(!player.isDead) {
						allDead = false;
						break;
					}
				}
				
				if(allDead) {
					//show gameover
					dead.SetActive(true);
					again.SetActive(true);
				}
			}
			else if(hit.collider.tag == "Goal") {
				win.SetActive(true);
				again.SetActive(true);
				hasWinner = true;
			}
		}
		
		if(!isDefaultMove) {
			players[nextPlayer].makeTurn();
		}
	}
			
	void moveTarget(Vector2 newLocalPos) {
		transform.GetComponentsInChildren<movetargets>(true)[0].transform.localPosition = newLocalPos;
		GetComponentsInChildren<movetargets>(true)[0].OnMove();
	}
	
	void setTargetVisible(bool visible) {
		transform.GetComponentsInChildren<movetargets>(true)[0].gameObject.SetActive(visible);
		pretrace.gameObject.SetActive(visible);
	}
}
