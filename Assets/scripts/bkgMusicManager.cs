using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class bkgMusicManager : MonoBehaviour
{

	void Awake(){

		if (GameObject.FindGameObjectsWithTag("soundManager").Length==1){ //this current soundmanager is the only one
			Debug.Log("this is the only soundManager");
			GetComponent<AudioSource>().Play();
			DontDestroyOnLoad(transform.gameObject);
			//Toggle T = FindObjectOfType<Toggle>();
			//bool isPlaying = GetComponent<AudioSource>().isPlaying;
			//T.isOn = isPlaying;


		} else { //sound has already been initialized

			GameObject mng = GameObject.FindGameObjectWithTag("soundManager");
			bool isPlaying = mng.GetComponent<AudioSource>().isPlaying;
			Debug.Log("isplaying? = " + isPlaying);
			//set toggle to status of audio
			Toggle T = FindObjectOfType<Toggle>();
			T.isOn = isPlaying;
			T.GetComponent<toggleAudioMng>().syncSprite();

			Debug.Log("DESTROYING SOUNDMNG"); //destroy this soundmanager bc one already exists
			Destroy(gameObject);
		}

	}

	// public void init(){
	// 	GetComponent<AudioSource>().Play();
	// 	DontDestroyOnLoad(transform.gameObject);
	// }

	// public void setAudio(bool isOn){
	// 	soundOn = isOn;
	// 	if (isOn){
	// 		GetComponent<AudioSource>().Play();
	// 	} else {
	// 		GetComponent<AudioSource>().Pause();

	// 	}
	// }


	public void flip(){
		Debug.Log("flip Audio");
		//soundOn = !soundOn;

		if (!GetComponent<AudioSource>().isPlaying){
			Debug.Log("play music");
			GetComponent<AudioSource>().Play();
		} else{
			Debug.Log("pause music");
			GetComponent<AudioSource>().Pause();
		}
	}
}
