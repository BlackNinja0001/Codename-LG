using UnityEngine;
using System.Collections;

public class PlayerStatsManager : MonoBehaviour {

	private Player player;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getCurHealth(){
		return player.curHP;
	}

	public int getMaxHealth(){
		return player.maxHP;
	}

	public int getDmgMin(){
		return player.dmgMin;
	}

	public int getDmgMax(){
		return player.dmgMax;
	}

	public int getShield(){
		return player.shield;
	}

	public int curIFrames(){
		return player.curIFrames;
	}

	public int maxIFrames(){
		return player.maxIFrames;
	}
}
