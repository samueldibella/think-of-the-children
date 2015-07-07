using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int playerSpeed;
	GameObject manager;

	int tileScale;

	// Use this for initialization
	void Start () {
		manager = transform.parent.gameObject;
		tileScale = manager.GetComponent<GameState>().tileScale; //from manager
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.W)) {
			manager.SendMessage("playerMove", 'n');
		} else if (Input.GetKeyDown(KeyCode.A)) {
			manager.SendMessage("playerMove", 'w');
		} else if (Input.GetKeyDown(KeyCode.S)) {
			manager.SendMessage("playerMove", 's');
		} else if (Input.GetKeyDown(KeyCode.D)) {
			manager.SendMessage("playerMove", 'e');
		}
		
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) {
			this.audio.Play();
		}
	}
}
