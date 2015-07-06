using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public GameObject sand; 
	public GameObject player;
	public GameObject[,] tiles;
	Queue pendingMoves = new Queue();
	GameObject currentLocation;
	public int mapHeight; 
	public int mapWidth; 

	Vector3 firstTile; 
	public int tileScale; 
	int firstX; 
	int firstY; 
	int playerStartY;
	int globalTurn;
	int clockSpeed;
	bool playerMoving;
	

	// Use this for initialization
	void Start () {
		globalTurn = 0;
		mapHeight = 10;
		mapWidth = 10;
		
		firstX = 0 - mapWidth/2;
		firstY = 0 - mapHeight/2;
		tileScale = 1;
		firstTile = new Vector3(firstX, firstY, 0);
		clockSpeed = 20;
		playerMoving = false;
	
		tiles = new GameObject[mapHeight, mapWidth];
	
		for(int j = 0; j < mapHeight; j++) {
			for(int i = 0; i < mapWidth; i++) {
				tiles[j,i] = Instantiate(sand, new Vector3(firstTile.x + (i * tileScale), firstTile.y + (j * tileScale), firstTile.z), Quaternion.identity) as GameObject;
				tiles[j, i].transform.parent = this.transform;
			}
		}
		
		playerStartY = (int) Random.Range(0, (mapHeight - 1));
		currentLocation = tiles[firstTile + playerStartY, firstTileX] as GameObject;
		player = Instantiate(player, new Vector3(firstTile.x, firstTile.y + playerStartY, 0), Quaternion.identity) as GameObject;
		player.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		StartCoroutine ( turns() );
	}
	
	//from sent messages in player script
	void playerMove(char direction) {
		pendingMoves.Enqueue(direction);
	}
	
	IEnumerator Moves() {
		char currentDir = (char) pendingMoves.Dequeue();
		GameObject goalTile;
		
		switch(currentDir) {
			case 'n':
				break;
			case 'w':
				break;
			case 'e':
				break;
			case 's':
				break;
		}
		
		
		player.transform.position = Vector3.Lerp(player.transform.position, )
	}
	
	IEnumerator turns() {
		while(true) {
			if(pendingMoves.Count > 0) {
				StartCoroutine( Moves() );
				
				yield return new WaitForSeconds(clockSpeed);
			} else {
				yield return 0;
			}
		
			
		}
	}
	
}
