using UnityEngine;
using System.Collections;

public class TileStat : MonoBehaviour {

	public int x;
	public int y;
	public bool playerHere;
	public bool ObstacleHere;
	public bool isExit;
	public int radLevel;
	
	// Use this for initialization
	void Start () {
		playerHere = false;
		ObstacleHere = false;
		radLevel = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
