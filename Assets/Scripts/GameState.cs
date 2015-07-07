using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public GameObject tilePrefab; 
	public GameObject player;
	public GameObject[,] tiles;
	public Material exitLook;
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
		mapWidth = 20;
		
		firstX = 0 - mapWidth/2;
		firstY = 0 - mapHeight/2;
		tileScale = 1;
		firstTile = new Vector3(firstX, firstY, 0);
		clockSpeed = 20;
		playerMoving = false;
	
		tiles = new GameObject[mapHeight, mapWidth];
	
		for(int j = 0; j < mapHeight; j++) {
			for(int i = 0; i < mapWidth; i++) {
				tiles[j, i] = Instantiate(tilePrefab, new Vector3(firstTile.x + (i * tileScale), firstTile.y + (j * tileScale), firstTile.z), Quaternion.identity) as GameObject;
				tiles[j, i].transform.parent = this.transform;
				tiles[j, i].GetComponent<TileStat>().x = i;
				tiles[j, i].GetComponent<TileStat>().y = j;
			}
		}
		
		//set exit
		tiles[Random.Range(0, mapHeight - 1), mapWidth - 1].GetComponent<TileStat>().isExit = true;
		tiles[Random.Range(0, mapHeight - 1), mapWidth - 1].renderer.material = exitLook;
		
		playerStartY = (int) Random.Range(0, (mapHeight - 1));
		currentLocation = tiles[tiles[0,0].GetComponent<TileStat>().y + playerStartY, tiles[0,0].GetComponent<TileStat>().x];
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
	
	IEnumerator turns() {
		while(true) {
			if(pendingMoves.Count > 0 && !playerMoving) {
				char currentDir = (char) pendingMoves.Dequeue();
				GameObject goalTile = currentLocation;
				int goalX = currentLocation.GetComponent<TileStat>().x;
				int goalY = currentLocation.GetComponent<TileStat>().y;
				
				switch(currentDir) {
				case 'n':
					if(goalY < mapHeight - 1) {
						goalTile = tiles[goalY + 1, goalX];
					}
					break;
				case 'w':
					if(goalX > 0) {
						goalTile = tiles[goalY, goalX - 1];
					} 
					break;
				case 'e':
					if(goalX < mapWidth - 1) {
						goalTile = tiles[goalY, goalX + 1];
					} 
					break;
				case 's':
					if(goalY > 0) {
						goalTile = tiles[goalY - 1, goalX];
					}
					break;
				default:
					break;
				}
				
				playerMoving = true;
				player.transform.position = goalTile.transform.position;
				
				currentLocation.GetComponent<TileStat>().playerHere = false;
				goalTile.GetComponent<TileStat>().playerHere = false;
				
				currentLocation = goalTile;
				playerMoving = false;
			
			}
			
			yield return 0;
		}
	}
	
}
