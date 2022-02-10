using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teks: MonoBehaviour {
	public Text textMusic;

	public void Start() {
		if(Sound.Instance.music.mute == false) {
			textMusic.text = "ON";
		} else {
			textMusic.text = "OFF";
		}
	}

	public void ToogleMusicMute() {
		if(textMusic.text == "ON") {
			textMusic.text = "OFF";
		} else {
			textMusic.text = "ON";
		}

		Sound.Instance.ToogleMusic();
	}
}
