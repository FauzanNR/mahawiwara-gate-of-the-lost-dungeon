using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner: MonoBehaviour {

	public LayerMask layer;
	public int maxNPCNumber = 5;
	public Collider ground;
	[SerializeField] private GameObject npcPrefab;
	private List<GameObject> npcPool = new List<GameObject>();
	float scale = 1f;


	void Start() {
		/*var randomNumber = Random.Range( 1, maxNPCNumber );
		for(int i = 0; i < randomNumber; i++) {
			var obj = Instantiate( npcPrefab, new PositionHelper().generateRandomPosition( ground, scale, layer ), Quaternion.identity );
			obj.SetActive( false );
			npcPool.Add( obj );
		}*/
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			foreach(var npc in npcPool) {
				npc.SetActive( true );
			}
		}
	}

}
