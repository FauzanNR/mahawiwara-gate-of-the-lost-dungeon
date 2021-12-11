using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats: MonoBehaviour {
	public int att1;
	public int att2;
	public int att3;
	public int att4;

	public float colldownAtt2;
	public float colldownAtt3;
	public float colldownAtt4;

	private CombatCondition condition;

	void Start() {
		var player = GameObject.FindGameObjectWithTag( "Player" );
		condition = player.GetComponent<CombatCondition>();

	}
	public int Damage() {
		switch(condition.playerState) {
			case PLAYER_STATE.att1:
				print( att1 );
				return att1;
			case PLAYER_STATE.att2:
				return att2;
			case PLAYER_STATE.att3:
				return att3;
			case PLAYER_STATE.att4:
				return att4;
			default:
				return 0;
		}
	}
}
