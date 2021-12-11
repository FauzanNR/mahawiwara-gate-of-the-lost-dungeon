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

	void Awake() {
		getRoomState = GameObject.FindGameObjectWithTag( "LevelBuilder" ).GetComponent<LevelBuilder>();
	}
	//void Start() {
	//	generateNPC();
	//}
	void Update() {
		var isDone = getRoomState.RoomIsDone;
		if(isDone && npcPool.Count == 0) {
			print( "generateNPC" );
			generateNPC();
			return;
		}
	}

	void generateNPC() {

		var randomNumber = Random.Range( 1, maxNPCNumber );

		for(int i = 0; i < randomNumber; i++) {
			var obj = Instantiate( npcPrefab[Random.Range( 0, npcPrefab.Count - 1 )], new PositionHelper().generateRandomPosition( ground, scale, layer ), Quaternion.identity );
			obj.SetActive( false );
			npcPool.Add( obj );
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			foreach(var npc in npcPool) {
				npc.SetActive( true );
			}
		}
	}

}
