//Bug: Colliding on top of enemy = unable to jump (moving works to hilarious effect)

using UnityEngine;
using System.Collections;

public class Enemy : Damageable {


	//0 for attack on sight (stealth)
	//1 for attack immediately when player is on screen
	//2 for 
	public int attackMethod;

	// Use this for initialization
	void Start () {
		//(Temporary) Stats
 		curHP = 1;
		maxHP = 1;
		shield = 0;
		dmgMin = 1;
		dmgMax = 1;
		curIFrames = 0;
		maxIFrames = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Movement(){
		//startpoint, endpoint
	}

	void Attack(){
		
	}

	//Collisions with Player are handled in Player class
}
