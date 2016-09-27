using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject curChkPt;	//current checkpoint
	private Player player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Factor Damage to Player or Enemy if one is attacking another
	public void inflictDamage (Damageable attacker, Damageable defender) {

		//Calculate damage (uses attacker range)
		System.Random rnd = new System.Random();
		int dmg = rnd.Next(attacker.dmgMin, attacker.dmgMax+1);

		//If defender's shield prevents damage...
		if (defender.shield >= dmg) {
			//defender will not lose health, just some or all of shield
			defender.shield -= dmg;
		} else {
			//defender will lose some or all of health (and all of shield)
			defender.curHP = defender.curHP + defender.shield - dmg;
			defender.shield = 0;

		}

		//If defender is dead and...
		if (defender.curHP <= 0){ 
			//defender is player
			if (defender.name == "Player") {
				respawnPlayer ();
				//else if defender is enemy
			}else if (defender.tag == "Enemy"){
				//DESTROY!!!!
				Destroy(defender.gameObject);
			}
		}
	}

	public void respawnPlayer(){
		Debug.Log ("Respawning Player");
		//reset player health and shield
		player.curHP = player.maxHP;
		player.shield = 0;

		//moves player to checkpoint
		player.transform.position = curChkPt.transform.position;

		//stops camera from moving during death
		player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;


	}

	public void instantKill(Damageable dyingThing){
		//if dyingThing is player
		Debug.Log("Instant Kill");
		if (dyingThing.name == "Player") {
			//dyingThing.GetComponent<Rigidbody2D>().gravityScale = 0f;
			respawnPlayer ();
		}
	}
}
