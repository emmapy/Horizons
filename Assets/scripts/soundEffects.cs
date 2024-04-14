using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffects : MonoBehaviour
{

	public Clip[] clips;

	public AudioClip thisClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playClip(AudioClip clip){
    	AudioSource src = GetComponent<AudioSource>();
    	src.clip = clip;
    	src.Play();
    }

    public void init(){

    }
}

[System.Serializable]
public class Clip{

	public string myName;
	public AudioClip myClip;

	Clip(string name, AudioClip clip){
		myName = name;
		clip = myClip;
	}
}
