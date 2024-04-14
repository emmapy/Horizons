using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class hintMng : MonoBehaviour
{
	public int hintsLeft;
	public bool isOn;

	void Start(){
		transform.GetChild(hintsLeft).gameObject.SetActive(true);

	}
	public void flip(){
		if (hintsLeft == 0) return;

		isOn = !isOn;
		if (isOn){
			Debug.Log("waiting for selection");
			Debug.Log(GetComponent<Animator>());
			GetComponent<Animator>().SetBool("blink", true);
			//wait for click
		}
		if (!isOn){
			Debug.Log("turning off hint");
			GameObject.FindWithTag("message").SetActive(false);
			GetComponent<Animator>().SetBool("blink", false);

		}
	}

	public void decrement(){
		hintsLeft--;
		//update sprite
		//update emtpy
		for (int i = 0; i<4; i++){
			transform.GetChild(i).gameObject.SetActive(false);
		}
		transform.GetChild(hintsLeft).gameObject.SetActive(true);

		if (hintsLeft == 0){
			GetComponent<Button>().interactable = false;
		}

	}

}
