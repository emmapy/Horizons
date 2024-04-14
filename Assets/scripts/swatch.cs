using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swatch : MonoBehaviour
{

	public SpriteRenderer sprite;
	public peg solution;
    public Animator anim;
    public Vector2 loc;

    public Vector3 initScale;
    public float scaleChange;
    public bool animating;

    // Start is called before the first frame update
    void Start()
    {
        //sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {   
        // if (anim.GetCurrentAnimatorStateInfo(0).IsName("pickUp") || 
        //     anim.GetCurrentAnimatorStateInfo(0).IsName("drop") ) {
        //     gameObject.transform.localScale = new Vector3(initScale[0]*scaleChange, initScale[1]*scaleChange, 1);
        // } 

        // if (anim.GetCurrentAnimatorStateInfo(0).IsName("init")){
        //     gameObject.transform.localScale = new Vector3(0, 0, 0);
        //     Debug.Log(initScale + ", " + scaleChange);
        //     gameObject.transform.localScale = new Vector3(initScale[0]*scaleChange, initScale[1]*scaleChange, 1);

        // }

        // Debug.Log("1: " + gameObject.transform.localScale);
        // gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
        // Debug.Log("2: " + gameObject.transform.localScale);

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("default")){
            //Debug.Log("here");
            // gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            //Debug.Log(gameObject);
            //Debug.Break();
            gameObject.transform.localScale = new Vector3(initScale[0]*scaleChange, initScale[1]*scaleChange, 1);
            //Debug.Log("here: " + gameObject.transform.localScale);

            animating = true;
        } else {
            animating = false;
        }

        //Debug.Log("3: " + gameObject.transform.localScale);

        
    }

    public void init(Vector2 myLoc){
        //Debug.Log("init: " + myLoc);

        sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        initScale = gameObject.transform.localScale;
        loc = myLoc;


    }


}
