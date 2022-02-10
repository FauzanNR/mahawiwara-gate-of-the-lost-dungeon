using UnityEngine;
using System.Linq.Expressions;

public class EndRoom: Room {
	[SerializeField] NPCController npcBoss;
	private int level;

	void setVictory() {
		GameManager.Instance.UpdateGameState( GameStates.Victory );
		if(level == PlayerDataManager.player.level) PlayerDataManager.player.level += 1;
	}

	void Update() {
		if(npcBoss.enemyState == NPC_STATE.Death) {
			Invoke( nameof( setVictory ), 7 );
		}
	}

	void Start() {
		level = PlayerDataManager.player.level;
		GameManager.Instance.UpdateGameState( GameStates.Game );
		npcBoss.gameObject.transform.parent = null;
		npcBoss.gameObject.SetActive( true );
	}
	void OnDestroy() {
		if(npcBoss != null)
			Destroy( npcBoss.gameObject );
	}
}