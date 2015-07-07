using UnityEngine;
using System.Collections;

public class TileStat : MonoBehaviour {

	public int x;
	public int y;
	public bool playerHere;
	public bool ObstacleHere = false;
	public bool isExit;
	public int radLevel;
	
	public GameObject obstaclePrefab;
	GameObject myObstacle;
	
	// Use this for initialization
	void Start () {
		playerHere = false;
		radLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void spawnObstacle() {
		myObstacle = Instantiate(obstaclePrefab, this.transform.position, Quaternion.identity) as GameObject;
	}
}
