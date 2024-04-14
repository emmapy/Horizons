using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleChange : MonoBehaviour
{
	public Vector3 initScale;
	public float scale;
	public bool animating;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("default")){
    		gameObject.transform.localScale = new Vector3(initScale[0]*scale, initScale[1]*scale, 1);
    		animating = true;
    	} else {
    		animating = false;
    	}
        
    }
}
