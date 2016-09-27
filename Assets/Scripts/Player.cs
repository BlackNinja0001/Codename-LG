//First written 8/8/16 by JC
//Jumping code courtesy of http://forum.unity3d.com/threads/mario-style-jumping.381906/
//Movement courtesy of Official Unity 2D Platformer Tutorial example project

using UnityEngine;
using System.Collections;

public class Player : Damageable {
	/*these floats are the force you use to jump, the max time you want your jump to be allowed to happen,
     * and a counter to track how long you have been jumping*/
	public float jumpForce;
	public float jumpTime;
	public float jumpTimeCounter;
	/*this bool is to tell us whether you are on the ground or not
     * the layermask lets you select a layer to be ground; you will need to create a layer named ground(or whatever you like) and assign your
     * ground objects to this layer.
     * The stoppedJumping bool lets us track when the player stops jumping.*/
	public bool grounded;
	public LayerMask whatIsGround;
	public bool stoppedJumping;

	/*the public transform is how you will detect whether we are touching the ground.
     * Add an empty game object as a child of your player and position it at your feet, where you touch the ground.
     * the float groundCheckRadius allows you to set a radius for the groundCheck, to adjust the way you interact with the ground*/

	public Transform groundCheck;
	public float groundCheckRadius;

	//You will need a rigidbody to apply forces for jumping, in this case I am using Rigidbody 2D because we are trying to emulate Mario :)
	private Rigidbody2D rb;

	//Controls
	KeyCode jump1, jump2, shoot1, shoot2;

	private bool facingRight = true;			// For determining which way the player is currently facing.

	public float moveForce/* = 365f*/;			// Amount of force added to move the player left and right.
	public float maxSpeed/* = 5f*/;				// The fastest the player can travel in the x axis.
	//private Animator anim;					// Reference to the player's animator component.

	public ProjectileManager projMngr;
	public GameObject curProjectile; 			//The projectile launched every time the player presses the shoot button

	void Awake (){
		//anim = GetComponent<Animator>();
	}

	void Start () {
		//Change for controls if needed
		jump1 = KeyCode.Z;
		jump2 = KeyCode.K;
		shoot1 = KeyCode.X;
		shoot2 = KeyCode.L;

		//sets the jumpCounter to whatever we set our jumptime to in the editor
		jumpTimeCounter = jumpTime;

		rb = GetComponent<Rigidbody2D> ();
		projMngr = FindObjectOfType<ProjectileManager>();

		//Stats
		curHP = 3;
		maxHP = 3;
		dmgMin = 1;
		dmgMax = 1;
		shield = 0;
		curIFrames = 0;
		maxIFrames = 90;
	}

	void Update() {
/**Jumping**/
		//determines whether our bool, grounded, is true or false by seeing if our groundcheck overlaps something on the ground layer
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);

		//if we are grounded...
		if(grounded)
		{
			//the jumpcounter is whatever we set jumptime to in the editor.
			jumpTimeCounter = jumpTime;
		}

/**Movement Controls**/
		//if you press down the jump button...
		if(Input.GetKeyDown (jump1) || Input.GetKeyDown(jump2))
		{
			//and you are on the ground...
			if(grounded)
			{
				//jump!
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
				stoppedJumping = false;
			}
		}

		//if you keep holding down the jump button...
		if((Input.GetKey(jump1) || Input.GetKey(jump2)) && !stoppedJumping)
		{
			//and your counter hasn't reached zero...
			if(jumpTimeCounter > 0)
			{
				//keep jumping!
				rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
				jumpTimeCounter -= Time.deltaTime;
			}
		}


		//if you stop holding down the mouse button...
		if(Input.GetKeyUp (jump1) || Input.GetKeyUp (jump2))
		{
			//stop jumping and set your counter to zero.  The timer will reset once we touch the ground again in the update function.
			jumpTimeCounter = 0;
			stoppedJumping = true;
		}

/**Shooting projectiles**/
		//if shoot button is pressed...
		if (Input.GetKeyDown (shoot1) || Input.GetKeyDown (shoot2)) {
			//Fuckin' shoot man!
			projMngr.Shoot(curProjectile, facingRight);

		}

/**Invincibility when hit**/
		if (curIFrames > 0) {
			curIFrames -= 1;
		}


	}


	void FixedUpdate () {
/**Walking/Running**/
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");

		// The Speed animator parameter is set to the absolute value of the horizontal input.
		//anim.SetFloat("Speed", Mathf.Abs(h));

		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			// ... add a force to the player.
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();

		
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	//Changes
	void switchWeapon(){

	}

	void OnTriggerEnter2D(Collider2D other){
/**Falling below the level**/
		if (other.name == "Death Pit") {
			//kill player
			lvlMngr.instantKill(this);
		}

/**Touching enemies**/
		//if hit (enemy/projectile/weapon collides with player) and player is not currently invincible
		if (other.gameObject.tag == "Enemy" && curIFrames <= 0){
			Debug.Log ("Bad touch! Bad touch!");

			//Enable invincibility
			curIFrames = maxIFrames;

			//Subtract from health accordingly
			if (this.shield > 0){
				this.shield -= 1;
			} else {
				this.curHP -= 1;
				//If dead
				if (this.curHP <= 0){
					//respawn
					lvlMngr.respawnPlayer();
				}
			}
		}

/**Picking up items**/
		if (other.gameObject.tag == "Item"){

		}
	}
}
