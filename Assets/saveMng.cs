using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveMng : MonoBehaviour
{
	public bool newPlayer;
    // Start is called before the first frame update
    void Start()
    {
    	Debug.Log("starting");

    	if (newPlayer) {
            GetComponent<player>().loadNew();
            GetComponent<player>().save();
        }

    	else GetComponent<player>().load();

    	DontDestroyOnLoad(transform.gameObject);
    	levelgroupMng m = FindObjectOfType<levelgroupMng>();
    	m.init();

        if (FindObjectsOfType<saveMng>().Length == 1){ //this current saveMng is the only one
			Debug.Log("this is the only saveMng");

		} else { //we are returning to the menu from another scene, destory the other one
			saveMng[] s = FindObjectsOfType<saveMng>();
			Destroy(s[1].gameObject);

		}

		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
