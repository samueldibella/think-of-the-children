using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public GameObject Manager;
	public GameObject player;
	
	public float lerpSpeed;
	public float cameraAngleOffset;
	Vector2 lerpDir;

	// Use this for initialization
	void Start () {
		player = Manager.GetComponent<GameState>().player;
		
		lerpSpeed = .1f;
		cameraAngleOffset = .3f;
	}
	
	// Update is called once per frame
	void Update () {
		lerpDir = Vector2.Lerp(transform.position, player.transform.position, lerpSpeed);
		
		transform.position = new Vector3(lerpDir.x - cameraAngleOffset, lerpDir.y - cameraAngleOffset, transform.position.z);
	}
}
