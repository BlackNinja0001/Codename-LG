using UnityEngine;
using System.Collections;

public class ProjectileManager : MonoBehaviour {

	public int bulletForce;						// How fast the projectile is moving
	public Transform firePoint;					// Where the projectile is instantiated

	private readonly int bulletLimit = 3;		// Max amount of bullets allowed on screen
	public int curBulletsOnScreen;				// How many bullets are currently on screen
	public int shootDelay;						// How many milliseconds that must elapse between each firing of the projectile
	public float bulletLifeInSecs;				// How long in seconds the projectile stays on screen

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (shootDelay > 0) {
			shootDelay -= 1;
		}
	}

	public void launchProjectile(GameObject projectile, bool facingRight){
		//Instantiate bullet in front of player's weapon
		GameObject bulletInstance;
		bulletInstance = (GameObject)Instantiate(projectile, firePoint.position, firePoint.rotation);	//need 	to do object pooling later

		//add force to bullet
		Vector2 force;
		if (facingRight) {
			force = new Vector2 (bulletForce, 0);
		} else {
			force = new Vector2 (-bulletForce, 0);
		}

		if (bulletInstance != null) {
			//Debug.Log ("It's time to launch the cannon!");
			bulletInstance.GetComponent<Rigidbody2D>().AddForce (force);
		} else {
			Debug.Log ("This baby ain't flyin.");
		}

		//make it disappear either after a set amount of time, a set amount of distance, or if it hits an enemy
	}

	public void Shoot(GameObject projectile, bool facingRight){
		if (shootDelay <= 0) {
			shootDelay = 10;
			//Make sure number of bullets on screen does not surpass bullet limit
			if (curBulletsOnScreen < bulletLimit) {
				curBulletsOnScreen += 1;
				//Debug.Log ("curBulletsOnScreen = " + curBulletsOnScreen);

				launchProjectile (projectile, facingRight);
			}
		}
	}

}
