using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class toggleAudioMng : MonoBehaviour
{
	public GameObject MusicOn;
	public GameObject MusicOff;


		// void Awake(){
		// 	GameObject obj = GameObject.FindWithTag("soundManager");
		// 	GetComponent<Toggle>().isOn = obj.GetComponent<AudioSource>().isPlaying;

		// }

    // Start is called before the first frame update
	public void toggleAudio(){
		GameObject obj = GameObject.FindWithTag("soundManager");
		//Debug.Log("toggleAudio: " + obj);
		Debug.Log("new val: " + GetComponent<Toggle>().isOn);
		if (GetComponent<Toggle>().isOn){ //play music
			if (!obj.GetComponent<AudioSource>().isPlaying) obj.GetComponent<AudioSource>().Play();
		} else { //otherwise, pause it
			obj.GetComponent<AudioSource>().Pause();
		}
		Debug.Log("audioPlaying? " + obj.GetComponent<AudioSource>().isPlaying);
		//obj.GetComponent<bkgMusicManager>().flip();
	}


	public void syncSprite(){
		Debug.Log("syncSprite: check mark should be: " + GetComponent<Toggle>().isOn);
		//Debug.Log("syncSprite: toggle graphic: " + GetComponent<Toggle>().graphic);
		if (GetComponent<Toggle>().isOn){
			MusicOn.SetActive(true);
			MusicOff.SetActive(false);
		} else {
			MusicOn.SetActive(false);
			MusicOff.SetActive(true);
		}

	}
}
