using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public LevelManager lvlMng;

	// Use this for initialization
	void Start () {
		lvlMng = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Registers checkpoint player collided with as new checkpoint
	void OnTriggerEnter2D (Collider2D other) {
		if (other.name == "Player") {
			lvlMng.curChkPt = gameObject;
			Debug.Log ("Checkpoint reached.");
		}
	}
}
