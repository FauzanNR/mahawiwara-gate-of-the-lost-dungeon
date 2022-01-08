using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NPCSpawner: MonoBehaviour {

	public LayerMask layer;
	public int maxNPCNumber = 5;
	public Collider ground;
	[SerializeField] private List<GameObject> npcPrefab;
	private List<GameObject> npcPool = new List<GameObject>();
	float scale = 1f;
	LevelBuilder getRoomState;
	bool roomDone = false;


	void Awake() {
		GameManager.OnStateChange += roomIsDone;
	}

	private void roomIsDone(GameStates obj) {
		roomDone = (obj == GameStates.Game);
	}

	void OnDestroy() {
		GameManager.OnStateChange -= roomIsDone;
	}
	void Update() {

		if(roomDone && npcPool.Count == 0) {
			generateNPC();
		}
	}

	void generateNPC() {

		var randomNumber = UnityEngine.Random.Range( 1, maxNPCNumber );

		for(int i = 0; i < randomNumber; i++) {
			var obj = Instantiate( npcPrefab[UnityEngine.Random.Range( 0, npcPrefab.Count - 1 )], new PositionHelper().generateRandomPosition( ground, scale, layer ), Quaternion.identity );
			obj.SetActive( false );
			npcPool.Add( obj );
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			foreach(var npc in npcPool) {
				if(npc != null) npc.SetActive( true );
			}
		}
	}

}
