using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public Vector3 initScale;
	//public float scaleChange;
	private bool isActive = false;
    //public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        //anim = new Animator();
        //initScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // if (anim.GetCurrentAnimatorStateInfo(0).IsName("pickUp") || 
        //     anim.GetCurrentAnimatorStateInfo(0).IsName("drop") ) {
        //     gameObject.transform.localScale = new Vector3(initScale[0]*scaleChange, initScale[1]*scaleChange, 1);
        // } 

        // if (anim.GetCurrentAnimatorStateInfo(0).IsName("init")){
        //     //gameObject.transform.localScale = new Vector3(0, 0, 0);
        //     gameObject.transform.localScale = new Vector3(initScale[0]*scaleChange, initScale[1]*scaleChange, 1);

        // }
    }


    void OnMouseDown(){
        if (!enabled) {
            //gameObject.GetComponent<Animator>().SetTrigger("badMove");
            //gameObject.GetComponent<Animator>().Play("badMove", 0, 0f);
            gameObject.GetComponent<swatch>().anim.Play("badMove", 0, 0f);
            return;
        }

        hintMng h = FindObjectOfType<hintMng>();
        if (h.isOn){ 
            checkManager c = FindObjectOfType<checkManager>();
            // peg p = GetComponent<peg>();
            // c.revealHint(p);
            peg p = c.getPegUnder(GetComponent<swatch>());
            c.revealHint(p);
            h.flip();
            h.decrement();
            return;
        }

        movement active = getActive();
        if (active == null || isActive == true){ //regular pick up or drop
            flipActive();
        } else {
            //swap with the active swatch
            swapPos(active);
            active.flipActive();
        }
    }

    public void flipActive(){
    	if (isActive == false){
            //pick up
            Debug.Log("****PICK UP LISTENER****");
    		gameObject.tag = "active";
    	} else if (isActive == true){
            //drop
            Debug.Log("****DROP LISTENER****");
    		//gameObject.transform.localScale *= (1/scaleChange);
    		gameObject.tag = "Untagged";
    	}
       gameObject.GetComponent<swatch>().anim.SetBool("selected", !isActive); //******CONTROLS ANIMATOR
        //gameObject.GetComponent<swatch>().anim.Play("pickUp", 0, 0f); //******CONTROLS ANIMATOR

    	isActive = !isActive;
    }

    public void moveHere(Vector3 destination){
        Debug.Log("movehere");
        gameObject.transform.position = destination;
    }

    movement getActive(){
        movement active = null;
        GameObject tagged = GameObject.FindWithTag("active");
        if (tagged == null) return active;
        active = tagged.GetComponent<movement>();
        return active;
    }

    void swapPos(movement destination){
        Vector3 tempPos = gameObject.transform.position;
        gameObject.transform.position = destination.transform.position;
        destination.transform.position = tempPos;
    }

}
