using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager: MonoBehaviour {
	public GameObject[] Berkah;
	public int level = 0;
	public LevelBuilder levelBuilder;

	GameObject getBerkah() => Berkah[Random.Range( 0, Berkah.Length - 1 )];

	void Update() {
		var isDone = levelBuilder.RoomIsDone;
		if(isDone) {
			level += 1;
			print( "generate berkah, level" );
			return;
		}
	}
}
