using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public GameObject tilePrefab; 
	public GameObject impassablePrefab;
	public GameObject player;
	public GameObject[,] tiles;
	public Material exitLook;
	Queue pendingMoves = new Queue();
	GameObject currentLocation;
	public int mapHeight; 
	public int mapWidth; 
	public int impassableFrequency;

	Vector3 firstTile; 
	public int tileScale; 
	int firstX; 
	int firstY; 
	int playerStartY;
	
	int globalTurn;
	int clockSpeed;
	bool playerMoving;
	

	// Use this for initialization
	void Awake () {
		globalTurn = 0;
		mapHeight = 100;
		mapWidth = 100;
		
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
		
		for(int j = 0; j < mapHeight; j++) {
			for(int i = 1; i < mapWidth; i++) {
				if(Random.Range(0, 100) < impassableFrequency && tiles[j, i].GetComponent<TileStat>().isExit == false) {
					tiles[j, i].GetComponent<TileStat>().spawnObstacle();
					tiles[j, i].GetComponent<TileStat>().ObstacleHere = true;
					
				} 	
			}
		}
		
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
					if(goalY < mapHeight - 1 && tiles[goalY + 1, goalX].GetComponent<TileStat>().ObstacleHere == false) {
						goalTile = tiles[goalY + 1, goalX];
					}
					break;
				case 'w':
					if(goalX > 0 && tiles[goalY, goalX - 1].GetComponent<TileStat>().ObstacleHere == false) {
						goalTile = tiles[goalY, goalX - 1];
					} 
					break;
				case 'e':
					if(goalX < mapWidth - 1 && tiles[goalY, goalX + 1].GetComponent<TileStat>().ObstacleHere == false) {
						goalTile = tiles[goalY, goalX + 1];
					} 
					break;
				case 's':
					if(goalY > 0 && tiles[goalY - 1, goalX].GetComponent<TileStat>().ObstacleHere == false) {
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
			
				globalTurn++;
			}
			
			yield return 0;
		}
	}
	
}
