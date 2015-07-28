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
	public GameObject whiteTreePrefab;
	GameObject myObstacle;
	
	// Use this for initialization
	void Start () {
		playerHere = false;
		radLevel = Random.Range(0, 10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void spawnObstacle(char type) {
		int orientationAngle = Random.Range(0, 4) * 90;
		float[] scaleDeviation = new float[3];
		
		for(int i = 0; i < 3; i++) {
			scaleDeviation[i] = Random.Range(-2, 2) * .1f;
		} 

		switch(type) {
			case 'w':
			myObstacle = Instantiate(whiteTreePrefab, this.transform.position, Quaternion.identity) as GameObject;
				break;
			case 'n':
			myObstacle = Instantiate(obstaclePrefab, this.transform.position, Quaternion.identity) as GameObject;
				break;
			default:
				break;
		}	
		
		myObstacle.transform.Rotate(0, 0,  orientationAngle);
		myObstacle.transform.localScale += new Vector3(scaleDeviation[0], scaleDeviation[1], scaleDeviation[2]);
	}
}
