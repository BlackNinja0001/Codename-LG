using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour {

	public int 	curHP, maxHP, 						//health
				shield, 							//extra HP that goes beyond maxHP
				dmgMin, dmgMax, 					//damage inflicted by this object whenever it lands an attack
				curIFrames, maxIFrames;				//How many frames where the Damageable is invincible

	public LevelManager lvlMngr;

	// Use this for initialization
	void Start () {
		lvlMngr = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if (curHP > maxHP) {
			curHP = maxHP;
		}
	}

	//Default constructor
	public Damageable(){
		curHP = 1;
		maxHP = 1;
		shield = 0;
		dmgMin = 1;
		dmgMax = 1;
		curIFrames = 0;
		maxIFrames = 5;
	}

	//Custom constructor
	public Damageable (	int custCurHP, int custMaxHP,
						int custShield, 
						int custDmgMin, int custDmgMax, 
						int custCurIFrames, int custMaxIFrames){
		curHP = custCurHP;
		maxHP = custMaxHP;
		shield = custShield;
		dmgMin = custDmgMin;
		dmgMax = custDmgMax;
		curIFrames = custCurIFrames;
		maxIFrames = custMaxIFrames;
	}
}
