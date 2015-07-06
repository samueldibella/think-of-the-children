using UnityEngine;
using System.Collections;

public class GameState : MonoBehaviour {

	public GameObject sand; 
	public GameObject player;
	public GameObject[,] tiles;
	public int mapHeight; 
	public int mapWidth; 

	
	int firstX; 
	int firstY; 
	int tileScale; 
	int playerStartY;
	Vector3 firstTile; 

	// Use this for initialization
	void Start () {
		mapHeight = 10;
		mapWidth = 10;
		
		firstX = 0 - mapWidth/2;
		firstY = 0 - mapHeight/2;
		tileScale = 1;
		firstTile = new Vector3(firstX, firstY, 0);
	
		tiles = new GameObject[mapHeight, mapWidth];
	
		for(int j = 0; j < mapHeight; j++) {
			for(int i = 0; i < mapWidth; i++) {
				tiles[j,i] = Instantiate(sand, new Vector3(firstTile.x + (i * tileScale), firstTile.y + (j * tileScale), firstTile.z), Quaternion.identity) as GameObject;
				tiles[j, i].transform.parent = this.transform;
			}
		}
		
		playerStartY = (int) Random.Range(0, (mapHeight - 1));
		player = Instantiate(player, new Vector3(firstTile.x, firstTile.y + playerStartY, 0), Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
