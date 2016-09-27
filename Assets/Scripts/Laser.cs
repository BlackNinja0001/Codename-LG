using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	private ProjectileManager projMngr;
	private LevelManager lvlMngr;
	private Player player;

	// Use this for initialization
	void Start () {
		projMngr = FindObjectOfType<ProjectileManager>();
		lvlMngr = FindObjectOfType<LevelManager> ();
		player = FindObjectOfType<Player> ();

		//Stats for laser guitar (change if needed)
		projMngr.bulletForce = 1000;
		projMngr.shootDelay = 10;
		projMngr.bulletLifeInSecs = 0.5f;

		//Bullet will disappear after a set amount of time
		if (this.name == "Laser(Clone)") {
			projMngr.curBulletsOnScreen -= 1;
			Destroy (this.gameObject, projMngr.bulletLifeInSecs);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other){
		Debug.Log ("Yeah, lemons!");
		//Bullet will disappear if it hits anything
		Destroy (gameObject);

		//Reduce enemy's health if bullet hits an enemy
		if (other.gameObject.tag == "Enemy") {
			Debug.Log ("We hit the enemy ship, me hearties!");
			Enemy enemy = other.gameObject.GetComponent<Enemy> ();
			//Bug: gameObjects cannot be cast to Damageable
			//Maybe try using gameObject.SendMessage()?
			lvlMngr.inflictDamage (player, enemy);
		}
	}
}
