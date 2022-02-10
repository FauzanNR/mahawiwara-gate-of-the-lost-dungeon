using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound: MonoBehaviour {
	public static Sound Instance {
		get; set;
	}

	[HideInInspector]
	public AudioSource music;
	//[HideInInspector]
	//public AudioSource sfx;

	private void Awake() {
		music = transform.GetChild( 0 ).GetComponent<AudioSource>();
		//sfx = transform.GetChild(1).GetComponent<AudioSource>();



		if(Instance == null) {
			Instance = this;
		} else {
			Destroy( gameObject );
		}
	}
	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void ToogleMusic() {
		if(music.mute == false) {
			music.mute = true;
		} else {
			music.mute = false;
		}
	}

	//public void ToogleSFX()
	//{
	//    if (sfx.mute == false)
	//    {
	//        sfx.mute = true;
	//    }
	//    else
	//    {
	//        sfx.mute = false;
	//    }
	//}

	//public void ButtonSFX()
	//{
	//    sfx.Play();
	//}
}
