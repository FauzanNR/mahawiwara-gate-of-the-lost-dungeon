using UnityEngine;

public class EndRoom: Room {
	[SerializeField] NPCController npcBoss;
	[SerializeField] GameObject reward;

	void Update() {
		if(npcBoss.enemyState == NPC_STATE.Death) {
			GameManager.Instance.UpdateGameState( GameStates.Victory );
			var rewardSpawnPos = npcBoss.gameObject.transform;
			var newReward = Instantiate( reward, rewardSpawnPos );
		}
	}

	void Start() {
		GameManager.Instance.UpdateGameState( GameStates.Game );
		npcBoss.gameObject.transform.parent = null;
		npcBoss.gameObject.SetActive( true );
	}
	void OnDestroy() {
		if(npcBoss.gameObject != null)
			Destroy( npcBoss.gameObject );
	}
}