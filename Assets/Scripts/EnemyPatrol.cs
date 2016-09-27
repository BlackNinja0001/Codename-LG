using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour {

	public float moveSpeed;
	public bool moveRight;

	public bool grounded;
	public LayerMask whatIsGround;
	public Transform groundCheck;
	public float groundCheckRadius;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 rb2dvel = GetComponent<Rigidbody2D> ().velocity;

/**Enemy Movement AI**/
		//moves enemy on the x-axis while keeping its y constant (i.e. keep the enemy moving just left and right)
		//if enemy should move right...
		if (moveRight) {
			//move it right
			rb2dvel = new Vector2 (moveSpeed, rb2dvel.y);
		} else {
			//move it left
			rb2dvel = new Vector2 (-moveSpeed, rb2dvel.y);
		}
	}
}
