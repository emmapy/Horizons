using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour //the surface level "player" containing # of levels
{

	public int totalLevels;
	public bool[] completed;
	public bool[] unlocked;

	public void save(){
		saveSystem.savePlayer(this);
	}

	public void load(){
		playerData data = saveSystem.loadPlayer(); //get data

		if (data!=null){ //assign data values to this player
			totalLevels = data.totalLevels;
			//totalLevels = GameObject.FindWithTag("levels").transform.childCount;
			completed = new bool[totalLevels];
			unlocked = new bool[totalLevels];

			for (int i = 0; i<totalLevels; i++){
				completed[i] = data.completed[i];
				unlocked[i] = data.unlocked[i];
			}
		} else { //load "starting" player (a new player)
			loadNew();
		}
	}

	public void loadNew(){
		//Debug.Log(GameObject.FindWithTag("levels"));
		totalLevels = GameObject.FindWithTag("levels").transform.childCount;
		completed = new bool[totalLevels];
		unlocked = new bool[totalLevels];
		unlocked[0] = true;
	}
}
