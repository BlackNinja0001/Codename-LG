using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	//0 for Health
	//1 for Stat Upgrade
	//2 for Weapon
	public int KindOfPickup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){

		//if item is health
			//Increase Health
		//if item is stat upgrade
			//upgrade stat (duh!)
		//if item is weapon
			//Show a short, fancy-schmancy cutscene
			//Tell what weapon does
			//Allow weapon for use
	}
}
