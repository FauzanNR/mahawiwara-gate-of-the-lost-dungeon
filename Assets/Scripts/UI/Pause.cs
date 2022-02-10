using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause: MonoBehaviour {
	public Text textMusic;
	//public Text textSFX;

	void Start() {
		if(Sound.Instance.music.mute == false) {
			textMusic.text = "ON";
		} else {
			textMusic.text = "OFF";
		}

		//if (Sound.Instance.sfx.mute == false)
		//{
		//    textSFX.text = "ON";
		//}
		//else
		//{
		//    textSFX.text = "OFF";
		//}
	}

	public void ToogleMusicMute() {
		if(textMusic.text == "ON") {
			textMusic.text = "OFF";
		} else {
			textMusic.text = "ON";
		}

		Sound.Instance.ToogleMusic();
	}

	//public void ToogleSFXMute()
	//{
	//    if (textSFX.text == "ON")
	//    {
	//        textSFX.text = "OFF";
	//    }
	//    else
	//    {
	//        textSFX.text = "ON";
	//    }

	//    Sound.Instance.ToogleSFX();
	//}

	public void Scene(string nameScene) {
		SceneManager.LoadScene( nameScene );
	}

	public void QuitGame() {
		Application.Quit();
	}

	//public void ButtonSFX()
	//{
	//    Sound.Instance.ButtonSFX();
	//}
}
