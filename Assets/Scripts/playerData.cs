using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class playerData //raw variables that can be read by player
{
	public int totalLevels;
	public bool[] completed;
	public bool[] unlocked;

	public playerData(player p){
		totalLevels = p.totalLevels;
		completed = new bool[totalLevels];
		unlocked = new bool[totalLevels];

		for (int i = 0; i<totalLevels; i++){
			completed[i] = p.completed[i];
			unlocked[i] = p.unlocked[i];
		}
	}
}