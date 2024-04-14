using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public bool soundOn;

    // Start is called before the first frame update
    void Start()
    {
    	//Debug.Break();
        //check for scenemanager
        bkgMusicManager m = FindObjectOfType<bkgMusicManager>();
        if (m!=null) soundOn = m.GetComponent<AudioSource>().isPlaying;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GetComponent<AudioSource>().isPlaying);
        
    }

    public void runScene(string scene){
    	StartCoroutine(finishAudioAndRunScene(scene));
    }

    public void openPopUp(GameObject menu){
    	menu.SetActive(true);
    }

    public void closePopUp(GameObject menu){
    	menu.SetActive(false);
    }

    public void restartBoard(){
    	GameObject g = GameObject.FindWithTag("sceneManager");
    	sceneManager m = g.GetComponent<sceneManager>();
    	m.initSwatches();
    }

    public void playClip(AudioClip A){
    	Debug.Log("1) attempting to play clip: " + A + ", soundOn = " + soundOn + ", audiosource = " +  gameObject.GetComponent<AudioSource>().enabled);
        if (soundOn){
            gameObject.GetComponent<AudioSource>().clip = A;
            gameObject.GetComponent<AudioSource>().Play();
        }
        Debug.Log("2) attempting to play clip: " + A + ", soundOn = " + soundOn + ", audiosource = " +  gameObject.GetComponent<AudioSource>().enabled);

    }

    public void toggleAudio(){
        Debug.Log("toggle audio");
        soundOn = !soundOn;
    }

    IEnumerator finishAudioAndRunScene(string scene){
    	Debug.Log("waiting, audio is : " + gameObject.GetComponent<AudioSource>().isPlaying);
    	yield return new WaitWhile(()=>gameObject.GetComponent<AudioSource>().isPlaying);
    	Debug.Log("done waiting, run scene");
    	//SceneManager.LoadScene(scene);
    	SceneManager.LoadSceneAsync(scene);
    }
}
